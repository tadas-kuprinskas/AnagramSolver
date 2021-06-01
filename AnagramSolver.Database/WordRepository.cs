using AnagramSolver.BusinessLogic.StaticHelpers;
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

        public Dictionary<string, List<Word>> ReadAndGetDictionary()
        {
            Dictionary<string, List<Word>> dictionary = new();

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
                    AddWordToDictionary(dictionary, orderedWord, firstWord, partOfSpeech);
                }
            }
            return dictionary;
        }

        private static void AddWordToDictionary(Dictionary<string, List<Word>> anagrams, string orderedWord, string firstWord, 
            string partOfSpeech)
        {
            if (anagrams.ContainsKey(orderedWord))
            {
                anagrams[orderedWord].Add(
                    Mapping.MapToWord(orderedWord, firstWord, partOfSpeech));
            }
            else
            {
                anagrams.Add(orderedWord, new List<Word>()
                {
                    Mapping.MapToWord(orderedWord, firstWord, partOfSpeech)
                });
            }
        }
    }
}
