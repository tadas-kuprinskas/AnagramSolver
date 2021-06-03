using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class ValidationService : IValidationService
    {
        private readonly WordHandlingOptions _wordHandlingOptions;

        public ValidationService(IOptions<WordHandlingOptions> wordHandlingOptions)
        {
            _wordHandlingOptions = wordHandlingOptions.Value;
        }

        public void ValidateInputLength(string myWord)
        {
            if (myWord.Length < _wordHandlingOptions.MinInputLength)
            {
                throw new ArgumentException($"Input cannot be shorter than {_wordHandlingOptions.MinInputLength} letters");
            }
        }

        public void ValidateSingleWordAnagrams(IEnumerable<Word> words, string myWord)
        {
            if (words == null)
            {
                throw new ArgumentException($"There are no anagrams for your word \"{myWord}\"");
            }
        }
    }
}
