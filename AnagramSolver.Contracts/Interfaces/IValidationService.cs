using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IValidationService
    {
        void ValidateInputLength(string myWord);
        void ValidateSingleWordAnagrams(IEnumerable<Word> words, string myWord);
    }
}