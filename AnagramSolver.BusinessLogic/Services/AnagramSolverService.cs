using AnagramSolver.BusinessLogic.Exceptions;
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
        private readonly Utilities.Settings _options;

        public AnagramSolverService(IWordRepository wordRepository, IValidationService validationService, IOptions<Utilities.Settings> wordHandlingOptions)
        {
            _wordRepository = wordRepository;
            _validationService = validationService;
            _options = wordHandlingOptions.Value;
        }

        public IEnumerable<Word> GetUniqueAnagrams(string myWord)
        {
            char[] charsToTrim = { '*', ' ', '\'' };
            var myWordTrimmed = myWord.Trim(charsToTrim);

            _validationService.ValidateInputLength(myWordTrimmed);

            var anagrams = FindMultipleWordsAnagrams(myWordTrimmed).Where(w => !myWordTrimmed.Split(" ").Contains(w.Value));
            
            if (!anagrams.Any())
            {
                return Enumerable.Empty<Word>();
            }

            return anagrams;
        }

        public IList<Word> FindSingleWordAnagrams(string orderedWord)
        {
            var anagrams = _wordRepository.ReadAndGetDictionary();

            if (anagrams.ContainsKey(orderedWord))
            {
                return anagrams[orderedWord].Take(_options.NumberOfAnagrams).ToList();
            }
            return Array.Empty<Word>();
        }

        public IList<Word> FindMultipleWordsAnagrams(string myWordTrimmed)
        {
            var words = myWordTrimmed.Split(" ");

            var anagrams = words.SelectMany(x => FindSingleWordAnagrams(String.Concat(x.ToLower().OrderBy(c => c))).OrderBy(w => w.PartOfSpeech)).ToList();

            return anagrams;
        }
    }
}
