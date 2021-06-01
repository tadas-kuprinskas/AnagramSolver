using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IValidationService
    {
        void ValidateInput(string myWord);
        bool ValidateNumberOfAnagrams(Dictionary<string, List<Word>> anagrams, string sortedWord);
    }
}