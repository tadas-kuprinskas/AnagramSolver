using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.EF.DatabaseFirst
{
    public class CachedWordRepositoryEF : ICachedWordRepository
    {
        private readonly AnagramSolverContext _context;

        public CachedWordRepositoryEF(AnagramSolverContext context)
        {
            _context = context;
        }

        public CachedWord AddCachedWord(string myWord)
        {
            var cachedWord = new CachedWord() 
            { 
                Value = myWord 
            };

            _context.CachedWords.Add(cachedWord);

            return cachedWord;
        }

        public void AddToAdditionalTable(Word word, CachedWord cachedWord)
        {
            var additionalItem = new WordCachedWordAdditional()
            {
                Word = word,
                CachedWord = cachedWord
            };
            _context.WordCachedWordAdditionals.Add(additionalItem);
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            var anagrams = _context.WordCachedWordAdditionals
                .Include(w => w.Word)
                .Include(x => x.CachedWord)
                .Where(w => w.CachedWord.Value == myWord).Select(x => x.Word).ToList();

            return anagrams;
        }

        public CachedWord SearchCachedWord(string myWord)
        {
            var cachedWord = _context.CachedWords.FirstOrDefault(x => x.Value == myWord);
            return cachedWord;
        }
    }
}
