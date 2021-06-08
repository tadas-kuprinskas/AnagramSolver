using AnagramSolver.BusinessLogic.Exceptions;
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
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolverService _anagramSolverService;

        public WordController(IWordRepository wordRepository, IAnagramSolverService anagramSolverService)
        {
            _wordRepository = wordRepository;
            _anagramSolverService = anagramSolverService;
        }

        [HttpGet]
        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize)
        {
            return _wordRepository.GetPaginatedWords(currentPage, pageSize);
        }

        [HttpGet("Anagrams")]
        public IEnumerable<string> GetUniqueAnagrams(string myWord)
        {
            return _anagramSolverService.GetUniqueAnagrams(myWord).Select(w => w.Value);
        }
    }
}
