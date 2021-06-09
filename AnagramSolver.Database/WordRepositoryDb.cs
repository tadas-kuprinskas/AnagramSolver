using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Repository
{
    public class WordRepositoryDb : IWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Settings _options;

        public WordRepositoryDb(IOptions<Settings> options)
        {
            _options = options.Value;
            _sqlConnection = new SqlConnection(_options.ConnectionString);
        }

        public IEnumerable<Word> GetAllWords()
        {
            var insertQuery = "Select * from Word";

            _sqlConnection.Open();

            SqlCommand cmd = new(insertQuery, _sqlConnection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            List<Word> words = new();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    words.Add(new Word()
                    {
                        Id = Convert.ToInt32(dataReader["Id"]),
                        Value = dataReader["Value"].ToString(),
                        PartOfSpeech = dataReader["PartOfSpeech"].ToString(),
                        OrderedValue = dataReader["OrderedValue"].ToString()
                    });
                }
            }
            _sqlConnection.Close();
            return words;
        }

        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize)
        {
            var itemsNumber = 50;

            var words = GetAllWords();
            int count = words.Count();
            itemsNumber = pageSize < 1 ? itemsNumber : pageSize;
            var totalPages = (int)Math.Ceiling(count / (double)itemsNumber);
            var pagenumber = currentPage > totalPages ? totalPages : currentPage;
            var firstWordId = (pagenumber - 1) * itemsNumber;
            var lastWordId = pagenumber * itemsNumber;

            _sqlConnection.Open();

            var insertQuery = $"Select * from Word where Id between {firstWordId + 1} and {lastWordId}";

            SqlCommand cmd = new(insertQuery, _sqlConnection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            List<string> paginatedWords = new();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    paginatedWords.Add(dataReader["Value"].ToString());
                }
            }
            _sqlConnection.Close();
            return paginatedWords;
        }

        public Dictionary<string, HashSet<Word>> ReadAndGetDictionary()
        {
            throw new NotImplementedException();
        }

        public void AddWordsToDatabase(Word word, int id)
        {
            var insertQuery = "INSERT INTO Word (Id, Value, PartOfSpeech, OrderedValue)" +
                "VALUES(@Id, @Value, @PartOfSpeech, @OrderedValue)";

            var sortedWord = String.Concat(word.Value.ToLower().OrderBy(c => c));

            _sqlConnection.Open();

            using (SqlCommand cmd = new(insertQuery, _sqlConnection))
            {
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@Value", word.Value));
                cmd.Parameters.Add(new SqlParameter("@PartOfSpeech", word.PartOfSpeech));
                cmd.Parameters.Add(new SqlParameter("@OrderedValue", sortedWord));
                cmd.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }
    }
}
