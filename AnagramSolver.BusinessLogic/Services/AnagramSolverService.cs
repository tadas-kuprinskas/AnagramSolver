using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverService : IAnagramSolverService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IValidationService _validationService;
        private readonly IConfiguration _configuration;

        public AnagramSolverService(IWordRepository wordRepository, IValidationService validationService, IConfiguration configuration)
        {
            _wordRepository = wordRepository;
            _validationService = validationService;
            _configuration = configuration;
        }

        public IEnumerable<Word> GetUniqueAnagrams(string myWord)
        {
            _validationService.ValidateInput(myWord);

            var orderedWord = String.Concat(myWord.ToLower().OrderBy(c => c));

            var anagrams = FindAnagrams(orderedWord);

            return anagrams.GroupBy(a => a.Value).Select(w => w.First()).ToList();
        }

        public IEnumerable<Word> FindAnagrams(string orderedWord)
        {
            var anagrams = _wordRepository.ReadAndGetDictionary();

            var wordHandlingOptions = new WordHandlingOptions();

            _configuration.GetSection(WordHandlingOptions.WordHandling).Bind(wordHandlingOptions);

            if (anagrams.ContainsKey(orderedWord))
            {
                if(_validationService.ValidateNumberOfAnagrams(anagrams, orderedWord) == true)
                {
                    return anagrams[orderedWord];
                }
                else
                {
                    return anagrams[orderedWord].Take(wordHandlingOptions.NumberOfAnagrams);
                }
            }
            return null;
        }
    }
}
