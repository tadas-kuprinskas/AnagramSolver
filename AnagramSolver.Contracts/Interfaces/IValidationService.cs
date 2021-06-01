using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IValidationService
    {
        void ValidateInput(string myWord);
        IEnumerable<Word> ValidateNumberOfAnagrams(Dictionary<string, List<Word>> anagrams, string sortedWord);
    }
}