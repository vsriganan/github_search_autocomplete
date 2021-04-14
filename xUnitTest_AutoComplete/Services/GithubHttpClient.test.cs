using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoComplete_GitHub_SearchAPI.Services;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using AutoComplete_GitHub_SearchAPI;
using System.Web;
using System.Net.Http;
using NSubstitute;
using AutoComplete_GitHub_SearchAPI.Models;

namespace xUnitTest_AutoComplete.Services
{
    public class GithubHttpClientTest
    {
        GithubHttpClient subject;
        GithubHttpClient mockSubject;
        Dictionary<string, string> memoryConfig;

        string searchTerm = "autocomplete";
        string sort = "";
        string order = "";
        int perPage = 10;
        int pageNumber = 1;

        public GithubHttpClientTest()
        {
            memoryConfig = new Dictionary<string, string>
            {
                {"GitHubBaseSearchAddress", StaticVariables.searchBaseAddress},
                {"GitHubUserAgent", StaticVariables.userAgent},
                {"GitHubAuthorization", StaticVariables.githubAuthorisation}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(memoryConfig).Build();

            subject = new GithubHttpClient(configuration);
            mockSubject = Substitute.For<GithubHttpClient>(configuration);
        }

        [Theory]
        [InlineData(GitSearchType.code)]
        [InlineData(GitSearchType.commits)]
        [InlineData(GitSearchType.issues)]
        [InlineData(GitSearchType.labels)]
        [InlineData(GitSearchType.repositories)]
        [InlineData(GitSearchType.topics)]
        [InlineData(GitSearchType.users)]
        public void GetHttpClient__returns_http_client_with_required_headers(GitSearchType searchType)
        {
            //Arrange

            //Act
            var httpClient = subject.GetHttpClient(searchType);

            //Assert
            httpClient.BaseAddress.AbsoluteUri.Should().Be(StaticVariables.searchBaseAddress);
            Assert.Contains("application/json", httpClient.DefaultRequestHeaders.GetValues("accept"));
            if (searchType == GitSearchType.commits)
            {
                Assert.Contains("application/vnd.github.cloak-preview", httpClient.DefaultRequestHeaders.GetValues("accept"));
            }
            else if (searchType == GitSearchType.topics)
            {
                Assert.Contains("application/vnd.github.mercy-preview+json", httpClient.DefaultRequestHeaders.GetValues("accept"));
            }
            else
            {
                Assert.Contains("application/vnd.github.v3+json", httpClient.DefaultRequestHeaders.GetValues("accept"));
            }
            Assert.Contains(StaticVariables.userAgent, httpClient.DefaultRequestHeaders.GetValues("User-Agent"));
            Assert.Contains(StaticVariables.githubAuthorisation, httpClient.DefaultRequestHeaders.GetValues("Authorization"));
        }

        [Theory]
        [InlineData(GitSearchType.repositories, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.repositories, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.repositories, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.repositories, "autocomplete", "", "asc", 10, 1)]
        [InlineData(GitSearchType.code, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.code, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.code, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.code, "autocomplete", "", "asc", 10, 1)]
        [InlineData(GitSearchType.commits, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.commits, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.commits, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.commits, "autocomplete", "", "asc", 10, 1)]
        [InlineData(GitSearchType.issues, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.issues, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.issues, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.issues, "autocomplete", "", "asc", 10, 1)]
        [InlineData(GitSearchType.topics, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.topics, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.topics, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.topics, "autocomplete", "", "asc", 10, 1)]
        [InlineData(GitSearchType.users, "autocomplete", "", "", 10, 1)]
        [InlineData(GitSearchType.users, "autocomplete", "", "", null, null)]
        [InlineData(GitSearchType.users, "autocomplete", "created_at", "asc", 10, 1)]
        [InlineData(GitSearchType.users, "autocomplete", "", "asc", 10, 1)]
        public void GetUriWithQueryStrings__return_search_uri_with_query_strings(GitSearchType gitSearchType, string searchTerm, string searchSort, string searchOrder, int? searchPerPage, int? searchPageNumber)
        {
            //Arrange
            var expectedString = $"{gitSearchType}?q={HttpUtility.UrlEncode(searchTerm)}";
            if(!string.IsNullOrEmpty(searchSort) && !string.IsNullOrEmpty(searchOrder))
            {
                expectedString += $"&sort={searchSort}&order={searchOrder}";
            }
            if(searchPerPage != null)
            {
                expectedString += $"&per_page={searchPerPage}";
            }
            if (searchPageNumber != null)
            {
                expectedString += $"&page={searchPageNumber}";
            }

            //Act
            var actualString = subject.GetUriWithQueryStrings(gitSearchType, searchTerm, searchSort, searchOrder, searchPerPage, searchPageNumber);

            //Assert
            actualString.Should().Be(expectedString);
        }

        [Fact]
        public void GetSearchResponse__returns_repository_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.repositories).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.repositories}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.repositories);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.repositories).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber));
        }

        [Fact]
        public void GetSearchResponse__returns_code_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.code).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.code}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.code, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.code);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.code, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.code).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.code, searchTerm, sort, order, perPage, pageNumber));
        }

        [Fact]
        public void GetSearchResponse__returns_commit_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.commits).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.commits}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.commits);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.commits).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber));
        }

        [Fact]
        public void GetSearchResponse__returns_issue_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.issues).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.issues}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.issues);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.issues).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber));
        }

        [Fact]
        public void GetSearchResponse__returns_topic_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.topics).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.topics}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.topics);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.topics).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber));
        }

        [Fact]
        public void GetSearchResponse__returns_user_search_results_from_github_repository_search_api()
        {
            //Arrange
            var mockHttpClient = Substitute.For<HttpClient>();
            mockHttpClient.BaseAddress = new Uri(StaticVariables.searchBaseAddress);
            mockSubject.GetHttpClient(GitSearchType.users).Returns(mockHttpClient);

            var uriWithQueryStrings = $"{GitSearchType.users}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockSubject.GetUriWithQueryStrings(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);

            //Act
            var result = mockSubject.GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockSubject.Received(1).GetHttpClient(GitSearchType.users);
            mockSubject.Received(1).GetUriWithQueryStrings(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber);
            mockSubject.GetHttpClient(GitSearchType.users).Received(1).GetAsync(mockSubject.GetUriWithQueryStrings(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber));
        }
    }
}
