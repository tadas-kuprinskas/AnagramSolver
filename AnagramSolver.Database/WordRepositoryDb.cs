using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
        public WordRepositoryDb()
        {
            var connectionString = "Server=.;Database=AnagramSolver;Trusted_Connection=True;";
            _sqlConnection = new SqlConnection(connectionString);
        }

        public IEnumerable<Word> GetAllWords()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetPaginatedWords(int currentPage, int pageSize)
        {
            throw new NotImplementedException();
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
