using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchInformationController : ControllerBase
    {
        private readonly ISearchInformationService _searchInformationService;

        public SearchInformationController(ISearchInformationService searchInformationService)
        {
            _searchInformationService = searchInformationService;
        }

        //[HttpGet]
        //public IEnumerable<SearchInformation> GetAllSearchHistory()
        //{
        //   
        //}
    }
}
