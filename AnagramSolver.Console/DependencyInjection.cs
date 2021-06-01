using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Console.Writers;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Console
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            services.Configure<WordHandlingOptions>(configuration.GetSection(
                                        WordHandlingOptions.WordHandling));

            services.AddSingleton<AnagramSolverCLI>()
                    .AddSingleton<IAnagramSolverService, AnagramSolverService>()
                    .AddSingleton<IWordRepository, WordRepository>()
                    .AddSingleton<IValidationService, ValidationService>()
                    .AddSingleton<IWriter, ConsoleWriter>()
                    .AddSingleton<IConfiguration>(configuration)
                    .BuildServiceProvider();
        }
    }
}
