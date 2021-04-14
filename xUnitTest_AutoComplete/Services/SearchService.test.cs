using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using AutoComplete_GitHub_SearchAPI.Interfaces;
using NSubstitute;
using AutoComplete_GitHub_SearchAPI.Services;
using AutoComplete_GitHub_SearchAPI;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using AutoComplete_GitHub_SearchAPI.Models;

namespace xUnitTest_AutoComplete.Services
{
    public class SearchServiceTest
    {
        IGithubHttpClient mockGithubHttpClient;
        SearchService mockSubject;
        SearchService subject;

        string uriWithQueryStrings;

        string searchTerm = "autocomplete";
        string sort = "";
        string order = "";
        int perPage = 10;
        int pageNumber = 1;

        public SearchServiceTest()
        {
            mockGithubHttpClient = Substitute.For<IGithubHttpClient>();
            mockSubject = Substitute.For<SearchService>(mockGithubHttpClient);
            subject = new SearchService(mockGithubHttpClient);

            #region Arrange
            uriWithQueryStrings = $"{GitSearchType.repositories}?q={HttpUtility.UrlEncode(searchTerm)}&per_page={perPage}&page={pageNumber}";
            mockGithubHttpClient.GetUriWithQueryStrings(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber).Returns(uriWithQueryStrings);


            #endregion
        }

        [Fact]
        public void GetRepositorySearchResponse__returns_search_results_from_github_repository_search_api()
        {
            //Arrange
            GitRepositorySearchResponse gitResponse = new GitRepositorySearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitRepositorySearchResponse> response = new GitBaseResponse<GitRepositorySearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitRepositorySearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetRepositorySearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetCodeSearchResponse__returns_search_results_from_github_code_search_api()
        {
            //Arrange
            GitCodeSearchResponse gitResponse = new GitCodeSearchResponse
            {
                name = "name"
            };
            GitBaseResponse<GitCodeSearchResponse> response = new GitBaseResponse<GitCodeSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCodeSearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetCodeSearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetCommitSearchResponse__returns_search_results_from_github_commit_search_api()
        {
            //Arrange
            GitCommitSearchResponse gitResponse = new GitCommitSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitCommitSearchResponse> response = new GitBaseResponse<GitCommitSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCommitSearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetCommitSearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetIssueSearchResponse__returns_search_results_from_github_issue_search_api()
        {
            //Arrange
            GitIssueSearchResponse gitResponse = new GitIssueSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitIssueSearchResponse> response = new GitBaseResponse<GitIssueSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitIssueSearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetIssueSearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetTopicSearchResponse__returns_search_results_from_github_topic_search_api()
        {
            //Arrange
            GitTopicSearchResponse gitResponse = new GitTopicSearchResponse
            {
                description = "description"
            };
            GitBaseResponse<GitTopicSearchResponse> response = new GitBaseResponse<GitTopicSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitTopicSearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetTopicSearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetUserSearchResponse__returns_search_results_from_github_user_search_api()
        {
            //Arrange
            GitUserSearchResponse gitResponse = new GitUserSearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitUserSearchResponse> response = new GitBaseResponse<GitUserSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitUserSearchResponse>() { gitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber).Returns(response);

            //Act
            var result = subject.GetUserSearchResponse(searchTerm, sort, order, perPage, pageNumber);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, sort, order, perPage, pageNumber);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetInitialSearchResponse__returns_search_results_from_github_search_api()
        {
            //Arrange
            GitRepositorySearchResponse gitRepoResponse = new GitRepositorySearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitRepositorySearchResponse> RepoResponse = new GitBaseResponse<GitRepositorySearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitRepositorySearchResponse>() { gitRepoResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, null, null, 1, 1).Returns(RepoResponse);

            GitCodeSearchResponse gitCodeResponse = new GitCodeSearchResponse
            {
                name = "name"
            };
            GitBaseResponse<GitCodeSearchResponse> CodeResponse = new GitBaseResponse<GitCodeSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCodeSearchResponse>() { gitCodeResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", null, null, 1, 1).Returns(CodeResponse);

            GitCommitSearchResponse gitCommitResponse = new GitCommitSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitCommitSearchResponse> CommitResponse = new GitBaseResponse<GitCommitSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCommitSearchResponse>() { gitCommitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, null, null, 1, 1).Returns(CommitResponse);

            GitIssueSearchResponse gitIssueResponse = new GitIssueSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitIssueSearchResponse> IssueResponse = new GitBaseResponse<GitIssueSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitIssueSearchResponse>() { gitIssueResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, null, null, 1, 1).Returns(IssueResponse);

            GitTopicSearchResponse gitTopicResponse = new GitTopicSearchResponse
            {
                description = "description"
            };
            GitBaseResponse<GitTopicSearchResponse> TopicResponse = new GitBaseResponse<GitTopicSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitTopicSearchResponse>() { gitTopicResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, null, null, 1, 1).Returns(TopicResponse);

            GitUserSearchResponse gitUserResponse = new GitUserSearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitUserSearchResponse> UserResponse = new GitBaseResponse<GitUserSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitUserSearchResponse>() { gitUserResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, null, null, 1, 1).Returns(UserResponse);

            AutoCompleteSearchResponse response = new AutoCompleteSearchResponse
            {
                search_term = searchTerm,
                repository_search = RepoResponse,
                code_search = CodeResponse,
                commit_search = CommitResponse,
                topic_search = TopicResponse,
                issue_search = IssueResponse,
                user_search = UserResponse
            };

            //Act
            var result = subject.GetInitialResponse(searchTerm);

            //Assert
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", null, null, 1, 1);
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.Received(1).GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, null, null, 1, 1);
            result.Result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public void GetInitialSearchResponse__returns_search_results_when_no_text_is_provided()
        {
            //Arrange
            GitRepositorySearchResponse gitRepoResponse = new GitRepositorySearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitRepositorySearchResponse> RepoResponse = new GitBaseResponse<GitRepositorySearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitRepositorySearchResponse>() { gitRepoResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, null, null, 1, 1).Returns(RepoResponse);

            GitCodeSearchResponse gitCodeResponse = new GitCodeSearchResponse
            {
                name = "name"
            };
            GitBaseResponse<GitCodeSearchResponse> CodeResponse = new GitBaseResponse<GitCodeSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCodeSearchResponse>() { gitCodeResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", null, null, 1, 1).Returns(CodeResponse);

            GitCommitSearchResponse gitCommitResponse = new GitCommitSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitCommitSearchResponse> CommitResponse = new GitBaseResponse<GitCommitSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitCommitSearchResponse>() { gitCommitResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, null, null, 1, 1).Returns(CommitResponse);

            GitIssueSearchResponse gitIssueResponse = new GitIssueSearchResponse
            {
                node_id = "node_id"
            };
            GitBaseResponse<GitIssueSearchResponse> IssueResponse = new GitBaseResponse<GitIssueSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitIssueSearchResponse>() { gitIssueResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, null, null, 1, 1).Returns(IssueResponse);

            GitTopicSearchResponse gitTopicResponse = new GitTopicSearchResponse
            {
                description = "description"
            };
            GitBaseResponse<GitTopicSearchResponse> TopicResponse = new GitBaseResponse<GitTopicSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitTopicSearchResponse>() { gitTopicResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, null, null, 1, 1).Returns(TopicResponse);

            GitUserSearchResponse gitUserResponse = new GitUserSearchResponse
            {
                id = 1
            };
            GitBaseResponse<GitUserSearchResponse> UserResponse = new GitBaseResponse<GitUserSearchResponse>
            {
                total_count = 1,
                incomplete_results = false,
                items = new List<GitUserSearchResponse>() { gitUserResponse }
            };
            mockGithubHttpClient.GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, null, null, 1, 1).Returns(UserResponse);

            AutoCompleteSearchResponse response = new AutoCompleteSearchResponse
            {
                search_term = searchTerm,
                repository_search = RepoResponse,
                code_search = CodeResponse,
                commit_search = CommitResponse,
                topic_search = TopicResponse,
                issue_search = IssueResponse,
                user_search = UserResponse
            };

            //Act
            var result = subject.GetInitialResponse("");

            //Assert
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitRepositorySearchResponse>>(GitSearchType.repositories, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitCodeSearchResponse>>(GitSearchType.code, searchTerm + " org:github", null, null, 1, 1);
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitCommitSearchResponse>>(GitSearchType.commits, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitIssueSearchResponse>>(GitSearchType.issues, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitTopicSearchResponse>>(GitSearchType.topics, searchTerm, null, null, 1, 1);
            mockGithubHttpClient.DidNotReceive().GetSearchResponse<GitBaseResponse<GitUserSearchResponse>>(GitSearchType.users, searchTerm, null, null, 1, 1);
            result.Result.Should().BeNull();
        }
    }
}
