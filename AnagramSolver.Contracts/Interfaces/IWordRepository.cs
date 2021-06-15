using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Dictionary<string, HashSet<Word>> ReadAndGetDictionary();
        IEnumerable<Word> GetPaginatedWords(int currentPage, int pageSize, IEnumerable<string> words, string myWord);
        IEnumerable<Word> GetAllWords();
        void AddWordsToDatabase(Word word);
        IEnumerable<Word> SearchForWords(string myWord);
        Word GetWord(string myWord);
        void SaveChanges();
    }
}