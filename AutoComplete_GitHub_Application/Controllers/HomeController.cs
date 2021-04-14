using AutoComplete_GitHub_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AutoComplete_GitHub_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult Repository(GitBaseResponse<GitRepositorySearchResponse> data)
        {
            return PartialView(data);
        }

        public PartialViewResult Code(GitBaseResponse<GitCodeSearchResponse> data)
        {
            return PartialView(data);
        }

        public PartialViewResult Commit(GitBaseResponse<GitCommitSearchResponse> data)
        {
            return PartialView(data);
        }

        public PartialViewResult Issue(GitBaseResponse<GitIssueSearchResponse> data)
        {
            return PartialView(data);
        }

        public PartialViewResult Topic(GitBaseResponse<GitTopicSearchResponse> data)
        {
            return PartialView(data);
        }

        public PartialViewResult User(GitBaseResponse<GitUserSearchResponse> data)
        {
            return PartialView(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
