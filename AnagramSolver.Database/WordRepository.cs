﻿using AnagramSolver.BusinessLogic.Exceptions;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AnagramSolver.Database
{
    public class WordRepository : IWordRepository
    {
        private readonly Settings _options;

        public WordRepository(IOptions<Settings> options)
        {
            _options = options.Value;
        }

        public Dictionary<string, HashSet<Word>> ReadAndGetDictionary()
        {
            var path = PathGetting.GetFilePath("AnagramSolver\\" + _options.FilePath);

            Dictionary<string, HashSet<Word>> dictionary = new();

            using FileStream fileStream = File.Open(path, FileMode.Open);
            using StreamReader sr = new(fileStream);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] values = line.Split("\t");

                var firstWord = values[0];
                var secondWord = values[2];
                var partOfSpeech = values[1];

                var orderedFirstWord = String.Concat(firstWord.ToLower().OrderBy(c => c));
                var orderedSecondWord = String.Concat(secondWord.ToLower().OrderBy(c => c));

                if (orderedFirstWord != null)
                {
                    AddWordToDictionary(dictionary, orderedFirstWord, firstWord, partOfSpeech);
                }
  
                if (orderedSecondWord != null)
                {
                    AddWordToDictionary(dictionary, orderedSecondWord, secondWord, partOfSpeech);
                }              
            }
            return dictionary;
        }

        private static void AddWordToDictionary(Dictionary<string, HashSet<Word>> anagrams, string orderedWord, string word, 
            string partOfSpeech)
        {
            if (anagrams.ContainsKey(orderedWord))
            {
                anagrams[orderedWord].Add(
                Mapping.MapToWord(orderedWord, word, partOfSpeech));                     
            }
            else
            {
                anagrams.Add(orderedWord, new HashSet<Word>()
                {
                    Mapping.MapToWord(orderedWord, word, partOfSpeech)
                });
            }
        }

        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize)
        {
            var itemsNumber = 50;
            var words = ReadAndGetDictionary().Values.SelectMany(w => w.Select(x => x.Value)).ToList();

            int count = words.Count;
            itemsNumber = pageSize < 1 ? itemsNumber : pageSize;

            var totalPages = (int)Math.Ceiling(count / (double)itemsNumber);

            var pagenumber = currentPage > totalPages ? totalPages : currentPage;

            var items = words.Skip((pagenumber - 1) * itemsNumber).Take(itemsNumber).ToList();

            if (!items.Any())
            {
                return Enumerable.Empty<string>();
            }

            return items;
        }
    }
}
