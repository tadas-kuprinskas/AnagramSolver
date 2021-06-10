using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServiceDb
    {
        IEnumerable<string> SearchForWords(string myWord);
        IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize, IEnumerable<string> words, string myWord);
    }
}