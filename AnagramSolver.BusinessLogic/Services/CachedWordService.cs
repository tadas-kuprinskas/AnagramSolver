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
        private readonly IWordRepository _wordRepository;

        public CachedWordService(ICachedWordRepository cachedWordRepository, IWordRepository wordRepository)
        {
            _cachedWordRepository = cachedWordRepository;
            _wordRepository = wordRepository;
        }

        public void InsertCachedWordIntoTables(string myWord, List<Word> anagrams)
        {
            CachedWord newCachedWord = new()
            {
                Value = myWord
            };

            var cachedWord = _cachedWordRepository.AddCachedWord(newCachedWord);

            foreach (var anagram in anagrams)
            {
                var word = _wordRepository.GetWord(anagram.Value);

                _cachedWordRepository.AddToAdditionalTable(word, cachedWord);
            }

            _wordRepository.SaveChanges();
        }

        public CachedWord SearchCachedWord(string myWord)
        {
            var cachedWord = _cachedWordRepository.SearchCachedWord(myWord);
            return cachedWord;
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            return _cachedWordRepository.GetCachedAnagrams(myWord);
        }
    }
}
