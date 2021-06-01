using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IConfiguration _configuration;

        public ValidationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ValidateInput(string myWord)
        {
            var wordHandlingOptions = new WordHandlingOptions();
            _configuration.GetSection(WordHandlingOptions.WordHandling).Bind(wordHandlingOptions);

            if (myWord.Length < wordHandlingOptions.MinInputLength)
            {
                throw new ArgumentException($"Input cannot be shorter than {wordHandlingOptions.MinInputLength}");
            }
        }
    }
}
