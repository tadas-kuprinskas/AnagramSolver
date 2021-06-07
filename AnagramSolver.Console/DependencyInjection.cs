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
            var solutionPath = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;

            int index = solutionPath.IndexOf("\\AnagramSolver");
            solutionPath = solutionPath.Substring(0, index);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(solutionPath, "AnagramSolver/AnagramSolver.Console"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            services.Configure<Settings>(configuration.GetSection(
                                        Settings.HandlingOptions));

            services.AddScoped<AnagramSolverCLI>()
                    .AddScoped<IAnagramSolverService, AnagramSolverService>()
                    .AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IValidationService, ValidationService>()
                    .AddScoped<IWriter, ConsoleWriter>()
                    .AddScoped<IApiWordService, ApiWordService>();
        }
    }
}
