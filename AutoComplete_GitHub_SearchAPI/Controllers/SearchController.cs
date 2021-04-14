using AutoComplete_GitHub_SearchAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_SearchAPI.Controllers
{
    [Route("Search")]
    public class SearchController : Controller
    {
        ISearchService searchService;

        public SearchController(ISearchService service)
        {
            searchService = service;
        }

        [HttpGet]
        [Route("Repository")]
        public async Task<JsonResult> SearchRepositories(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetRepositorySearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("Code")]
        public async Task<JsonResult> SearchCode(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetCodeSearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("Commit")]
        public async Task<JsonResult> SearchCommit(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetCommitSearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("Issue")]
        public async Task<JsonResult> SearchIssue(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetIssueSearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("Topic")]
        public async Task<JsonResult> SearchTopic(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetTopicSearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("User")]
        public async Task<JsonResult> SearchUser(string searchTerm, string sort, string order, int? perPage, int? pageNumber)
        {
            var response = await searchService.GetUserSearchResponse(searchTerm, sort, order, perPage, pageNumber);
            return Json(response);
        }

        [HttpGet]
        [Route("All")]
        public async Task<JsonResult> SearchAllAPI(string searchTerm)
        {
            var response = await searchService.GetInitialResponse(searchTerm);
            return Json(response);
        }
    }
}
