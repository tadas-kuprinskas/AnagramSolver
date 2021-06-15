using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class WordServiceDb : IWordServiceDb
    {
        private readonly IWordRepository _wordRepository;

        public WordServiceDb(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IEnumerable<string> SearchForWords(string myWord)
        {
            return _wordRepository.SearchForWords(myWord).Select(w => w.Value);
        }

        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize, string myWord)
        {
            return _wordRepository.GetPaginatedWords(currentPage, pageSize, myWord).Select(w => w.Value);
        }
    }
}
