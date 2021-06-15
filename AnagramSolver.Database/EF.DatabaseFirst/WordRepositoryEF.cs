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
    public class WordRepositoryEF : IWordRepository
    {
        private readonly DataContext _context;

        public WordRepositoryEF(DataContext dataContext)
        {
            _context = dataContext;
        }

        public void AddWordsToDatabase(Word word)
        {
            var sortedWord = String.Concat(word.Value.ToLower().OrderBy(c => c));
            _context.Words.Add(word);
            _context.SaveChanges();
        }

        public IEnumerable<Word> GetAllWords()
        {
            return _context.Words.ToList();
        }

        public IEnumerable<Word> GetPaginatedWords(int currentPage, int pageSize, IEnumerable<string> words, string myWord)
        {
            var itemsNumber = 50;

            int count = words.Count();
            itemsNumber = pageSize < 1 ? itemsNumber : pageSize;

            var totalPages = (int)Math.Ceiling(count / (double)itemsNumber);

            var pagenumber = currentPage > totalPages ? totalPages : currentPage;

            var items = words.Skip((pagenumber - 1) * itemsNumber).Take(itemsNumber).Where(w => w.Contains(myWord));

            List<Word> wordList = new();

            foreach (var item in items)
            {
                wordList.Add(
                    new Word()
                    {
                        Value = item
                    });
            }

            if (!items.Any())
            {
                return Enumerable.Empty<Word>();
            }
            return wordList;
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

        public IEnumerable<Word> SearchForWords(string myWord)
        {
            var words = _context.Words.Where(w => w.Value.Contains(myWord));
            return words;
        }

        public Word GetWord(string wordToFind)
        {
            var word = _context.Words.First(x => x.Value == wordToFind);

            return word;
        }
    }
}
