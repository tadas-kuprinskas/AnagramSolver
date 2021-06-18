using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Data;
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
        private readonly AnagramSolverCodeFirstContext _context;

        public CachedWordRepositoryEF(AnagramSolverCodeFirstContext context)
        {
            _context = context;
        }

        public CachedWord AddCachedWord(CachedWord cachedWord)
        {
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
            _context.Entry<WordCachedWordAdditional>(additionalItem).State = EntityState.Added;
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
