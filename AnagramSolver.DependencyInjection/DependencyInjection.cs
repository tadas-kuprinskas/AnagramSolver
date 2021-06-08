using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
//using AnagramSolver.Console;
//using AnagramSolver.Console.Writers;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AnagramSolver.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var path = PathGetting.GetFilePath("AnagramSolver/AnagramSolver.Console");

            var configuration = new ConfigurationBuilder()
            .SetBasePath(path)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            services.Configure<Settings>(configuration.GetSection(
                                        Settings.HandlingOptions));

            services.AddScoped<IAnagramSolverService, AnagramSolverService>()
                    .AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IValidationService, ValidationService>()
                    .AddScoped<IClientService, AnagramClientService>()
                    .AddHttpClient();
        }
    }
}
