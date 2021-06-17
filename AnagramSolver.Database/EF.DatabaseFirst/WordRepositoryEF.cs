using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Data;
using AnagramSolver.EF.DatabaseFirst.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.EF.DatabaseFirst
{
    public class WordRepositoryEF : IWordRepository
    {
        private readonly AnagramSolverCodeFirstContext _context;

        public WordRepositoryEF(AnagramSolverCodeFirstContext dataContext)
        {
            _context = dataContext;
        }

        public void AddWordsToDatabase(Word word)
        {
            _context.Words.Add(word);
        }

        public IList<Word> GetAllWords()
        {
            return _context.Words.ToList();
        }

        public IList<Word> GetPaginatedWords(int currentPage, int pageSize, string myWord)
        {
            var items = _context.Words.Where(w => w.Value.Contains(myWord))
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize).ToList();

            return items;
        }

        public Dictionary<string, HashSet<Word>> ReadAndGetDictionary()
        {
            Dictionary<string, HashSet<Word>> dictionary = new();

            var words = _context.Words.ToList();

            foreach (var word in words)
            {
                AddWordToDictionary(dictionary, word.Id, word.OrderedValue, word.Value, word.PartOfSpeech);
            }

            return dictionary;
        }

        private static void AddWordToDictionary(Dictionary<string, HashSet<Word>> anagrams, int id, string orderedWord, string word,
            string partOfSpeech)
        {
            if (anagrams.ContainsKey(orderedWord))
            {
                anagrams[orderedWord].Add(
                new Word()
                {
                    Id = id,
                    Value = word,
                    PartOfSpeech = partOfSpeech,
                    OrderedValue = orderedWord
                });
            }
            else
            {
                anagrams.Add(orderedWord, new HashSet<Word>()
                {
                    new Word()
                    {
                        Id = id,
                        Value = word,
                        PartOfSpeech = partOfSpeech,
                        OrderedValue = orderedWord
                    }
                });
            }
        }

        public IList<Word> SearchForWords(string myWord)
        {
            var words = _context.Words.Where(w => w.Value.Contains(myWord)).ToList();
            return words;
        }

        public Word GetWord(string wordToFind)
        {
            var word = _context.Words.First(x => x.Value == wordToFind);

            return word;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
