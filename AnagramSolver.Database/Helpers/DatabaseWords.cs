using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Database;
using AnagramSolver.EF.DatabaseFirst.Data;
using AnagramSolver.Repository;
using AnagramSolver.Repository.EF.DatabaseFirst;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

            var services = new ServiceCollection();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(settings.ConnectionString));
            var serviceProvider = services.BuildServiceProvider();

            IWordRepository databaseWordsEF = new WordRepositoryEF(serviceProvider.GetService<DataContext>());

            //databaseWords.GetPaginatedWords(80, 20); testing
            //databaseWords.ReadAndGetDictionary(); testing

            var words = fileWords.GetAllWords().GroupBy(w => w.Value).Select(w => w.First()).ToList(); //for only unique words to be added to db

            foreach (var word in words)
            {
                databaseWordsEF.AddWordsToDatabase(word);
            }
        }
    }
}
