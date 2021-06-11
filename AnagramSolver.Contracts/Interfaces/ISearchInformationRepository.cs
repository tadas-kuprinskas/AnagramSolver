using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchInformationRepository
    {
        void AddSearchInformation(SearchInformation searchInformation);
    }
}