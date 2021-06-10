using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ICachedWordService
    {
        void InsertCachedWordIntoTables(string myWord, List<Word> anagrams);
        List<CachedWord> SearchCachedWord(string myWord);
        List<Word> GetCachedAnagrams(string myWord);
    }
}