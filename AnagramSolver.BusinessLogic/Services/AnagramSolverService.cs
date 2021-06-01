using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramSolverService : IAnagramSolverService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IWriter _consoleWriter;

        public AnagramSolverService(IWordRepository wordRepository, IWriter consoleWriter)
        {
            _wordRepository = wordRepository;
            _consoleWriter = consoleWriter;
        }

        public IEnumerable<Word> GetAnagrams(string myWord)
        {
            var orderedWord = String.Concat(myWord.ToLower().OrderBy(c => c));

            var anagrams = _wordRepository.GetAnagrams(orderedWord);

            return anagrams.GroupBy(a => a.Value).Select(w => w.First()).ToList();
        }

        public void PrintAnagrams(IEnumerable<Word> anagrams, string myWord)
        {
            foreach (var anagram in anagrams.Where(a => a.Value != myWord))
            {
                _consoleWriter.PrintLine(anagram.Value);
            }
        }
    }
}
