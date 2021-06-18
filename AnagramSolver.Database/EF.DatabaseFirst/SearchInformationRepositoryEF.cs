using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Data;
using AnagramSolver.EF.DatabaseFirst.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository.EF.DatabaseFirst
{
    public class SearchInformationRepositoryEF : ISearchInformationRepository
    {
        private readonly AnagramSolverCodeFirstContext _context;

        public SearchInformationRepositoryEF(AnagramSolverCodeFirstContext context)
        {
            _context = context;
        }

        public void AddSearchInformation(SearchInformation searchInformation)
        {
            _context.SearchInformations.Add(searchInformation);
        }

        public IList<SearchInformation> ReturnSearchInformation()
        {
            return _context.SearchInformations.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
