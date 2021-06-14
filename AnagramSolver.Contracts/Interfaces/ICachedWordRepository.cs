using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICachedWordRepository
    {
        CachedWord SearchCachedWord(string myWord);
        int AddCachedWord(string myWord);
        void AddToAdditionalTable(int wordId, int cachedWordId);
        List<Word> GetCachedAnagrams(string myWord);
    }
}