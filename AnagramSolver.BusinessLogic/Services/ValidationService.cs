using AnagramSolver.BusinessLogic.Exceptions;
using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class ValidationService : IValidationService
    {
        private readonly Settings _options;

        public ValidationService(IOptions<Settings> handlingOptions)
        {
            _options = handlingOptions.Value;
        }

        public void ValidateInputLength(string myWord)
        {
            if (myWord.Length < _options.MinInputLength)
            {
                throw new CustomException($"Input cannot be shorter than {_options.MinInputLength} letters");
            }
        }
    }
}
