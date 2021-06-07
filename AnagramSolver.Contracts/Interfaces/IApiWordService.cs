using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IApiWordService
    {
        IEnumerable<Word> GetPaginatedWords(int currentPage, int pageSize);
        Task<IEnumerable<Word>> SendGetAnagramsRequest(string myWord);
    }
}