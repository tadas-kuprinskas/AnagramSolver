using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public interface IAnagramSolverService
    {
        IEnumerable<Word> GetAnagrams(string input);
        void PrintAnagrams(IEnumerable<Word> anagrams, string myWord);
    }
}