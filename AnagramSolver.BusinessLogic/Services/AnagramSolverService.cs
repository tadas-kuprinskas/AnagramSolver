using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private readonly WordHandlingOptions _wordHandlingOptions;

        public AnagramSolverService(IWordRepository wordRepository, IValidationService validationService, IOptions<WordHandlingOptions> wordHandlingOptions)
        {
            _wordRepository = wordRepository;
            _validationService = validationService;
            _wordHandlingOptions = wordHandlingOptions.Value;
        }

        public IEnumerable<Word> GetUniqueAnagrams(string myWord)
        {
            char[] charsToTrim = { '*', ' ', '\'' };
            var myWordTrimmed = myWord.Trim(charsToTrim);

            _validationService.ValidateInputLength(myWordTrimmed);

            var orderedWord = String.Concat(myWordTrimmed.ToLower().OrderBy(c => c));

            return FindAnagrams(orderedWord);
        }

        public IEnumerable<Word> FindAnagrams(string orderedWord)
        {
            var anagrams = _wordRepository.ReadAndGetDictionary();

            if (anagrams.ContainsKey(orderedWord))
            {
                return anagrams[orderedWord].Take(_wordHandlingOptions.NumberOfAnagrams);
            }
            return null;
        }
    }
}
