using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using AnagramSolver.Repository;
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
            services.AddScoped<IAnagramSolverService, AnagramSolverService>()
                    .AddScoped<IWordRepository, WordRepositoryDb>()
                    //.AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IValidationService, ValidationService>()
                    .AddScoped<IClientService, AnagramClientService>()
                    .AddHttpClient();
        }
    }
}
