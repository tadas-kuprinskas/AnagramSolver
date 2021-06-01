using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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

        public AnagramSolverService(IWordRepository wordRepository, IValidationService validationService)
        {
            _wordRepository = wordRepository;
            _validationService = validationService;
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

            if (anagrams.ContainsKey(orderedWord))
            {
                return _validationService.ValidateNumberOfAnagrams(anagrams, orderedWord);
            }
            return null;
        }
    }
}
