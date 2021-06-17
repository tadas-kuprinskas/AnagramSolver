using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Dictionary<string, HashSet<Word>> ReadAndGetDictionary();
        IList<Word> GetPaginatedWords(int currentPage, int pageSize, string myWord);
        IList<Word> GetAllWords();
        void AddWordsToDatabase(Word word);
        IList<Word> SearchForWords(string myWord);
        Word GetWord(string myWord);
        void SaveChanges();
    }
}