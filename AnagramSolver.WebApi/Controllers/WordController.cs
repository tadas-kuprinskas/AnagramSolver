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
        private readonly IAnagramSolverService _anagramSolverService;
        private readonly ICachedWordService _cachedWordService;
        private readonly IWordServiceDb _wordServiceDb;
        private readonly ISearchInformationService _searchInformationService;

        public WordController(IAnagramSolverService anagramSolverService, ICachedWordService cachedWordService, 
            IWordServiceDb wordServiceDb, ISearchInformationService searchInformationService)
        {
            _anagramSolverService = anagramSolverService;
            _cachedWordService = cachedWordService;
            _wordServiceDb = wordServiceDb;
            _searchInformationService = searchInformationService;
        }

        [HttpGet]
        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize, string myWord)
        {
            var foundWords = _wordServiceDb.SearchForWords(myWord);
 
            return _wordServiceDb.GetPaginatedWords(currentPage, pageSize, foundWords, myWord); 
        }

        [HttpGet("Search")]
        public IEnumerable<string> GetUniqueAnagrams(string myWord)
        {
            var cachedWord = _cachedWordService.SearchCachedWord(myWord);       

            List<Word> anagrams;

            if (cachedWord == null)
            {
                 anagrams = _anagramSolverService.GetUniqueAnagrams(myWord).ToList();
                _cachedWordService.InsertCachedWordIntoTables(myWord, anagrams);
            }
            else 
            {
                anagrams = _cachedWordService.GetCachedAnagrams(myWord);
            }

            _searchInformationService.RecordSearchInformation(anagrams, myWord);

            return anagrams.Select(w => w.Value);
        }
    }
}
