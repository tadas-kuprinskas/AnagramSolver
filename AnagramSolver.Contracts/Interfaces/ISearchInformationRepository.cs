using AnagramSolver.Contracts.Models;
using System.Collections.Generic;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchInformationRepository
    {
        void AddSearchInformation(SearchInformation searchInformation);
        IEnumerable<SearchInformation> ReturnSearchInformation();
    }
}