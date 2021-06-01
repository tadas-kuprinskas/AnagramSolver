using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Dictionary<string, List<Word>> ReadAndGetDictionary();
    }
}