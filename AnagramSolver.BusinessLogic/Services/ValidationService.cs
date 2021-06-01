using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidateInput(string myWord)
        {
            if (myWord.Length < ConfigurationValues.GetMinInputLength())
            {
                throw new ArgumentException($"Input cannot be shorter than {ConfigurationValues.GetMinInputLength()}");
            }
        }

        public IEnumerable<Word> ValidateNumberOfAnagrams(Dictionary<string, List<Word>> anagrams, string sortedWord)
        {
            if (anagrams[sortedWord].Count > ConfigurationValues.GetNumberOfAnagrams())
            {
                return anagrams[sortedWord].Take(ConfigurationValues.GetNumberOfAnagrams());
            }
            else
            {
                return anagrams[sortedWord];
            }
        }
    }
}
