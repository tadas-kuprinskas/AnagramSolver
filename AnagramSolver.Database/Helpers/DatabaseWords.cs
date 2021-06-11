using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
            var settings = new Settings() { FilePath = "AnagramSolver.Contracts/Data/zodynas.txt", 
                ConnectionString = "Server=.;Database=AnagramSolver;Trusted_Connection=True;" };

            IOptions<Settings> options = Options.Create(settings);

            IWordRepository databaseWords = new WordRepositoryDb(options);

            IWordRepository fileWords = new WordRepository(options);

            //databaseWords.GetPaginatedWords(80, 20); testing
            //databaseWords.ReadAndGetDictionary(); testing

            var words = fileWords.GetAllWords().GroupBy(w => w.Value).Select(w => w.First()).ToList(); //for only unique words to be added to db

            foreach (var word in words)
            {
                databaseWords.AddWordsToDatabase(word);
            }
        }
    }
}
