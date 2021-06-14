using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
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
    public class CachedWordRepository : ICachedWordRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Settings _options;

        public CachedWordRepository(IOptions<Settings> options)
        {
            _options = options.Value;
            _sqlConnection = new SqlConnection(_options.ConnectionString);
        }

        public CachedWord SearchCachedWord(string myWord)
        {
            _sqlConnection.Open();
            var sqlQuery = "Select * from Cached_Word where Word_Value = @myWord";
            SqlCommand command = new(sqlQuery, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@myWord", myWord));
            SqlDataReader dataReader = command.ExecuteReader();
            var cachedWord = new CachedWord();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    cachedWord = new CachedWord()
                    {
                        Value = dataReader["Word_Value"].ToString(),
                        Id = int.Parse(dataReader["Id"].ToString())
                    };
                }
            }
            dataReader.Close();
            _sqlConnection.Close();
            return cachedWord;
        }

        public int AddCachedWord(string myWord)
        {
            _sqlConnection.Open();
            var insertQuery = "Insert into Cached_Word (Word_Value) output Inserted.Id values (@myWord)";
            SqlCommand command = new (insertQuery, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@myWord", myWord));
            var Id = int.Parse(command.ExecuteScalar().ToString());
            _sqlConnection.Close();

            return Id;
        }

        public void AddToAdditionalTable(int wordId, int cachedWordId)
        {
            _sqlConnection.Open();
            var sqlQueryinsert = "Insert into Word_CachedWord_Additional (Word_Id, CachedWord_Id) values (@WordId, @CachedWordId)";
            SqlCommand command = new (sqlQueryinsert, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@WordId", wordId));
            command.Parameters.Add(new SqlParameter("@CachedWordId", cachedWordId));
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            _sqlConnection.Open();

            var sqlQuery = "Select Word.Id, Word.Value from Cached_Word inner join Word_CachedWord_Additional on" +
                                    " Cached_Word.Id = Word_CachedWord_Additional.CachedWord_Id inner join Word on" +
                                    " Word_CachedWord_Additional.Word_Id = Word.Id where Cached_Word.Word_Value = @myWord";

            SqlCommand command = new(sqlQuery, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@myWord", myWord));
            SqlDataReader dataReader = command.ExecuteReader();
            var cachedWords = new List<Word>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    cachedWords.Add(new Word()
                    {
                        Id = int.Parse(dataReader["Id"].ToString()),
                        Value = dataReader["Value"].ToString(),
                    });
                }
            }
            dataReader.Close();
            _sqlConnection.Close();

            return cachedWords.Take(_options.NumberOfAnagrams).ToList();
        }
    }
}
