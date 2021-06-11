using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Database;
using AnagramSolver.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
                    .AddScoped<ICachedWordRepository, CachedWordRepository>()
                    .AddScoped<ICachedWordService, CachedWordService>()
                    .AddScoped<IWordServiceDb, WordServiceDb>()
                    //.AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IValidationService, ValidationService>()
                    .AddScoped<IClientService, AnagramClientService>()
                    .AddScoped<ISearchInformationService, SearchInformationService>()
                    .AddScoped<ISearchInformationRepository, SearchInformationRepository>()
                    .AddHttpClient()
                    .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
