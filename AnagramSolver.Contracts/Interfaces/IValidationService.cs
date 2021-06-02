using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IValidationService
    {
        void ValidateInputLength(string myWord);
    }
}