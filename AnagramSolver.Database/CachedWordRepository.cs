﻿using AnagramSolver.BusinessLogic.Utilities;
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
            var sqlQuery = "Select * from Cached_Words where Value = @myWord";
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
                        Value = dataReader["Value"].ToString(),
                        Id = int.Parse(dataReader["Id"].ToString())
                    };
                }
            }
            dataReader.Close();
            _sqlConnection.Close();
            return cachedWord;
        }

        public CachedWord AddCachedWord(string myWord)
        {
            _sqlConnection.Open();
            var insertQuery = "Insert into Cached_Words (Value) output Inserted.Id values (@myWord)";
            SqlCommand command = new (insertQuery, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@myWord", myWord));
            var Id = int.Parse(command.ExecuteScalar().ToString());
            _sqlConnection.Close();

            var cachedWord = new CachedWord() { Value = myWord, Id = Id };

            return cachedWord;
        }

        public void AddToAdditionalTable(Word word, CachedWord cachedWord)
        {
            _sqlConnection.Open();
            var sqlQueryinsert = "Insert into Word_CachedWord_Additionals (WordId, CachedWordId) values (@WordId, @CachedWordId)";
            SqlCommand command = new (sqlQueryinsert, _sqlConnection);
            command.Parameters.Add(new SqlParameter("@WordId", word.Id));
            command.Parameters.Add(new SqlParameter("@CachedWordId", cachedWord.Id));
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }

        public List<Word> GetCachedAnagrams(string myWord)
        {
            _sqlConnection.Open();

            var sqlQuery = "Select Words.Id, Words.Value from Cached_Words inner join Word_CachedWord_Additionals on" +
                                    " Cached_Words.Id = Word_CachedWord_Additionals.CachedWordId inner join Words on" +
                                    " Word_CachedWord_Additionals.WordId = Words.Id where Cached_Words.Value = @myWord";

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
