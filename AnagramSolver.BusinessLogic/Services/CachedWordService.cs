using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class CachedWordService : ICachedWordService
    {
        private readonly ICachedWordRepository _cachedWordRepository;

        public CachedWordService(ICachedWordRepository cachedWordRepository)
        {
            _cachedWordRepository = cachedWordRepository;
        }

        public void InsertCachedWordIntoTables(string myWord, List<Word> anagrams)
        {
            var id = _cachedWordRepository.AddCachedWord(myWord);

            foreach (var anagram in anagrams)
            {
                _cachedWordRepository.AddToAdditionalTable(anagram.Id, id);
            }
        }

        public CachedWord SearchCachedWord(string myWord)
        {
            return _cachedWordRepository.SearchCachedWord(myWord);
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            return _cachedWordRepository.GetCachedAnagrams(myWord);
        }
    }
}
