using AnagramSolver.BusinessLogic.Utilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository
{
    public class CachedWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Settings _options;

        public CachedWordRepository(IOptions<Settings> options)
        {
            _options = options.Value;
            _sqlConnection = new SqlConnection(_options.ConnectionString);
        }
    }
}
