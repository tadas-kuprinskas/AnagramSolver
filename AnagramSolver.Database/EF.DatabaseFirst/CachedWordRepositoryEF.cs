using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.EF.DatabaseFirst
{
    public class CachedWordRepositoryEF : ICachedWordRepository
    {
        private readonly DataContext _context;

        public CachedWordRepositoryEF(DataContext context)
        {
            _context = context;
        }

        public CachedWord AddCachedWord(string myWord)
        {
            var cachedWord = new CachedWord() 
            { 
                Value = myWord 
            };

            _context.Cached_Words.Add(cachedWord);

            return cachedWord;
        }

        public void AddToAdditionalTable(Word word, CachedWord cachedWord)
        {
            var additionalItem = new Word_CachedWord_Additionals()
            {
                Word = word,
                CachedWord = cachedWord
            };
            _context.Word_CachedWord_Additionals.Add(additionalItem);
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            var anagrams = from cachedWordVariable in _context.Cached_Words join word_cachedWordVariable 
                           in _context.Word_CachedWord_Additionals on cachedWordVariable equals word_cachedWordVariable.CachedWord
                           join wordVariable in _context.Words on word_cachedWordVariable.Word equals wordVariable
                           where cachedWordVariable.Value == myWord select wordVariable;

            return anagrams.ToList();
        }

        public CachedWord SearchCachedWord(string myWord)
        {
            var cachedWord = _context.Cached_Words.FirstOrDefault(x => x.Value == myWord);
            return cachedWord;
        }
    }
}
