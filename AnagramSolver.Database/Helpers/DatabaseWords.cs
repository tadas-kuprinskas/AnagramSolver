using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using AnagramSolver.Repository;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.Helpers
{
    public static class DatabaseWords
    {
        public static void AddWordsToDb()
        {
            IWordRepository databaseWords = new WordRepositoryDb();

            var settings = new Settings() { FilePath = "AnagramSolver.Contracts/Data/zodynas.txt" };
            IOptions<Settings> options = Options.Create(settings);

            IWordRepository fileWords = new WordRepository(options);

            var words = fileWords.GetAllWords();
            int id = 1;

            foreach (var word in words)
            {
                databaseWords.AddWordsToDatabase(word, id);
                id++;
            }
        }
    }
}
