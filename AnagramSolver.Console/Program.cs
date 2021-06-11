using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Console;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.DependencyInjection;
using AnagramSolver.Repository.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AnagramSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //DatabaseWords.AddWordsToDb();
            //StoredProcedure.RemoveTableData("Word"); //test

            var anagramSolverCli = GetAnagramSolverCLI();

            while (true)
            {
                anagramSolverCli.SendRequestAndExecute();
            }
        }

        private static void ConfigureAppSettings(ServiceCollection services)
        {
            var path = PathGetting.GetFilePath("AnagramSolver/AnagramSolver.Console");

            var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            services.Configure<Settings>(configuration.GetSection(
                                        Settings.HandlingOptions));
        }

        private static AnagramSolverCLI GetAnagramSolverCLI()
        {
            var services = new ServiceCollection();

            ConfigureAppSettings(services);

            AnagramSolver.DependencyInjection.DependencyInjection.ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var anagramSolverService = serviceProvider.GetService<IAnagramSolverService>();
            var apiWordService = serviceProvider.GetService<IClientService>();

            var anagramSolverCli = new AnagramSolverCLI(anagramSolverService, apiWordService);

            return anagramSolverCli;
        }
    }
}
