﻿using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Dictionary<string, HashSet<Word>> ReadAndGetDictionary();
        IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize);
        IEnumerable<Word> GetAllWords();
        void AddWordsToDatabase(Word word, int id);
    }
}