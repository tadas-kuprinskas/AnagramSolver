using AnagramSolver.BusinessLogic.StaticHelpers;
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
            var query = "Select * from Word";

            _sqlConnection.Open();

            SqlCommand cmd = new(query, _sqlConnection);
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
            dataReader.Close();
            _sqlConnection.Close();
            return words;
        }

        public IEnumerable<Word> GetPaginatedWords(int currentPage, int pageSize, IEnumerable<string> words, string myWord)
        {
            var itemsNumber = 5;

            currentPage = (currentPage == 0) ? 1 : currentPage;

            int count = words.Count();
            itemsNumber = pageSize < 1 ? itemsNumber : pageSize;
            var totalPages = (int)Math.Ceiling(count / (double)itemsNumber);
            var pageNumber = currentPage > totalPages ? totalPages : currentPage;
            var firstWord = (pageNumber - 1) * itemsNumber;

            _sqlConnection.Open();

            var query = "Select * from Word where Value like @myWord ORDER BY (Value) offset @firstWord rows fetch next @itemsNumber rows only";

            SqlCommand cmd = new(query, _sqlConnection);
            cmd.Parameters.Add(new SqlParameter("myWord", myWord + "%"));
            cmd.Parameters.Add(new SqlParameter("firstWord", firstWord));
            cmd.Parameters.Add(new SqlParameter("itemsNumber", itemsNumber));

            SqlDataReader dataReader = cmd.ExecuteReader();
            List<Word> foundWords = new();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    foundWords.Add(
                        new Word()
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Value = dataReader["Value"].ToString(),
                            PartOfSpeech = dataReader["PartOfSpeech"].ToString(),
                            OrderedValue = dataReader["OrderedValue"].ToString()
                        });
                }
            }
            dataReader.Close();
            _sqlConnection.Close();
            return foundWords;
        }

        public void AddWordsToDatabase(Word word)
        {
            var insertQuery = "Insert into Word (Value, PartOfSpeech, OrderedValue)" +
                              "VALUES(@Value, @PartOfSpeech, @OrderedValue)";
                

            var sortedWord = String.Concat(word.Value.ToLower().OrderBy(c => c));

            _sqlConnection.Open();

            using (SqlCommand cmd = new(insertQuery, _sqlConnection))
            {
                cmd.Parameters.Add(new SqlParameter("@Value", word.Value));
                cmd.Parameters.Add(new SqlParameter("@PartOfSpeech", word.PartOfSpeech));
                cmd.Parameters.Add(new SqlParameter("@OrderedValue", sortedWord));
                cmd.ExecuteNonQuery();
            }
            _sqlConnection.Close();
        }


        public Dictionary<string, HashSet<Word>> ReadAndGetDictionary()
        {
            var query = "Select * from Word";

            _sqlConnection.Open();

            SqlCommand cmd = new(query, _sqlConnection);
            SqlDataReader dataReader = cmd.ExecuteReader();

            Dictionary<string, HashSet<Word>> dictionary = new();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var id = Convert.ToInt32(dataReader["Id"]);
                    var word = dataReader["Value"].ToString();
                    var partOfSpeech = dataReader["PartOfSpeech"].ToString();
                    var orderedWord = dataReader["OrderedValue"].ToString();

                    AddWordToDictionary(dictionary, id, orderedWord, word, partOfSpeech);
                }
            }
            dataReader.Close();
            _sqlConnection.Close();
            return dictionary;
        }

        private static void AddWordToDictionary(Dictionary<string, HashSet<Word>> anagrams, int id, string orderedWord, string word,
            string partOfSpeech)
        {
            if (anagrams.ContainsKey(orderedWord))
            {
                anagrams[orderedWord].Add(
                new Word()
                {
                    Id = id,
                    Value = word,
                    PartOfSpeech = partOfSpeech,
                    OrderedValue = orderedWord
                });
            }
            else
            {
                anagrams.Add(orderedWord, new HashSet<Word>()
                {
                    new Word()
                    {
                        Id = id,
                        Value = word,
                        PartOfSpeech = partOfSpeech,
                        OrderedValue = orderedWord
                    }
                });
            }
        }

        public IEnumerable<Word> SearchForWords(string myWord)
        {
            var query = "Select * from Word where Value like @myWord";

            _sqlConnection.Open();

            List<Word> words = new();

            using (SqlCommand cmd = new(query, _sqlConnection))
            {
                cmd.Parameters.Add(new SqlParameter("myWord", myWord + "%"));
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        words.Add( 
                            new Word() 
                            {
                                Id = Convert.ToInt32(dataReader["Id"]),
                                Value = dataReader["Value"].ToString(),
                                PartOfSpeech = dataReader["PartOfSpeech"].ToString(),
                                OrderedValue = dataReader["OrderedValue"].ToString()
                            });
                    }
                }
                dataReader.Close();
            }
            _sqlConnection.Close();
            return words;
        }

        public Word GetWord(string myWord)
        {
            throw new NotImplementedException();
        }
    }
}
