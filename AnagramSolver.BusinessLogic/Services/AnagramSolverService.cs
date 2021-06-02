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

            if (myWordTrimmed.Split(" ").Length >= 2)
            {
                return FindMultipleWordsAnagrams(myWordTrimmed);
            }
            else if(myWordTrimmed.Split(" ").Length < 2)
            {
                var orderedWord = String.Concat(myWordTrimmed.ToLower().OrderBy(c => c));

                return FindSingleWordAnagrams(orderedWord);
            }
            else
            {
                return null;
            }          
        }

        public IEnumerable<Word> FindSingleWordAnagrams(string orderedWord)
        {
            var anagrams = _wordRepository.ReadAndGetDictionary();

            if (anagrams.ContainsKey(orderedWord))
            {
                return anagrams[orderedWord].Take(_wordHandlingOptions.NumberOfAnagrams);
            }
            return null;
        }

        public IEnumerable<Word> FindMultipleWordsAnagrams(string myWordTrimmed)
        {
            var words = myWordTrimmed.Split(" ");
            List<IEnumerable<Word>> listOfLists = new();
            List<Word> listOfWords = new();

            foreach (var word in words)
            {
                var orderedWord = String.Concat(word.ToLower().OrderBy(c => c));
                listOfLists.Add(FindSingleWordAnagrams(orderedWord));
            }

            foreach (var list in listOfLists)
            {
                foreach (var word in list)
                {
                    listOfWords.Add(word);
                }
            }

            return listOfWords;
        }
    }
}
