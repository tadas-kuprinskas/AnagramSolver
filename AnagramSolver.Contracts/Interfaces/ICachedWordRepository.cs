using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICachedWordRepository
    {
        CachedWord SearchCachedWord(string myWord);
        CachedWord AddCachedWord(string myWord);
        void AddToAdditionalTable(Word word, CachedWord cachedWord);
        List<Word> GetCachedAnagrams(string myWord);
    }
}