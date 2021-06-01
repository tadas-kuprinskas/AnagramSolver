using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.Database
{
    public class WordRepository : IWordRepository
    {
        private readonly string path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Contracts/Data/zodynas.txt");

        private readonly Dictionary<string, List<Word>> anagrams;

        public WordRepository()
        {
            anagrams = new Dictionary<string, List<Word>>();
            ReadAndAddToDictionary();
        }

        private void ReadAndAddToDictionary()
        {
            using FileStream fileStream = File.Open(path, FileMode.Open);
            using StreamReader sr = new(fileStream);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split("\t");

                var firstWord = values[0];
                var partOfSpeech = values[1];

                var orderedWord = String.Concat(firstWord.ToLower().OrderBy(c => c));

                if (orderedWord != null)
                {
                    AddWordToDictionary(orderedWord, firstWord, partOfSpeech);
                }
            }
        }

        private void AddWordToDictionary(string orderedWord, string firstWord, string partOfSpeech)
        {
            if (anagrams.ContainsKey(orderedWord))
            {
                anagrams[orderedWord].Add(
                    MapToWord(orderedWord, firstWord, partOfSpeech));
            }
            else
            {
                anagrams.Add(orderedWord, new List<Word>()
                {
                    MapToWord(orderedWord, firstWord, partOfSpeech)
                });
            }
        }

        private static Word MapToWord(string orderedWord, string firstWord, string partOfSpeech)
        {
            return new Word()
            {
                Value = firstWord,
                PartOfSpeech = partOfSpeech,
                OrderedValue = orderedWord
            };
        }

        public IEnumerable<Word> GetAnagrams(string sortedWord)
        {
            if (anagrams.ContainsKey(sortedWord))
            {
                return anagrams[sortedWord];
            }
            return null;
        }
    }
}
