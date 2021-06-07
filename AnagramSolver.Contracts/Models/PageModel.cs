using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Models
{
    public class PageModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public int TotalPages { get; set; }

        public bool HasPreviousPage { get { return (PageNumber > 1); } }

        public bool HasNextPage { get { return (PageNumber < TotalPages); } }
    }
}
