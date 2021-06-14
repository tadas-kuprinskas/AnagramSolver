﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
        private readonly DataContext _context;

        public SearchInformationRepositoryEF(DataContext context)
        {
            _context = context;
        }

        public void AddSearchInformation(SearchInformation searchInformation)
        {
            _context.SearchInformations.Add(searchInformation);
            _context.SaveChanges();
        }

        public IEnumerable<SearchInformation> ReturnSearchInformation()
        {
            return _context.SearchInformations.ToList();
        }
    }
}