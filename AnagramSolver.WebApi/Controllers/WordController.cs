using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace AnagramSolver.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordController : ControllerBase
    {
        private readonly IApiWordService _apiWordService;
        private readonly IAnagramSolverService _anagramSolverService;

        public WordController(IApiWordService apiWordService, IAnagramSolverService anagramSolverService)
        {
            _apiWordService = apiWordService;
            _anagramSolverService = anagramSolverService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Word>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetPaginatedWords(int currentPage, int pageSize)
        {
            try
            {
                return Ok(_apiWordService.GetPaginatedWords(currentPage, pageSize));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Anagrams")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetUniqueAnagrams(string myWord)
        {
            try
            {
                return Ok(_anagramSolverService.GetUniqueAnagrams(myWord).Select(w => w.Value));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
