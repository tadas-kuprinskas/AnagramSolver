using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Dictionary<string, HashSet<Word>> ReadAndGetDictionary();
        IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize, IEnumerable<string> words, string myWord);
        IEnumerable<Word> GetAllWords();
        void AddWordsToDatabase(Word word, int id);
        IEnumerable<string> SearchForWords(string myWord);
    }
}