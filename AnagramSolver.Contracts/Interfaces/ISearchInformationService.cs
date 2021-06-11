using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchInformationService
    {
        void RecordSearchInformation(List<Word> uniqueAnagrams, string myWord);
    }
}