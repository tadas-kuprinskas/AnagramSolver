using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Models;
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

        //public async Task<List<CachedWord>> GetByWord(string myWord)
        //{
        //    _sqlConnection.Open();
        //    var sqlQuery = "Select * from Cached_Word where Value = @myWord";
        //    SqlCommand command = new SqlCommand(sqlQuery, _sqlConnection);
        //    command.Parameters.Add(new SqlParameter("@Word", myWord));
        //    SqlDataReader dr = await command.ExecuteReaderAsync();
        //    //var cahcedWords = GenerateCachedWordsList(dr);
        //    _sqlConnection.Close();
        //    return cahcedWords;
        //}
    }
}
