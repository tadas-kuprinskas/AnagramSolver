using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolverService
    {
        IEnumerable<Word> GetUniqueAnagrams(string myWord);
    }
}