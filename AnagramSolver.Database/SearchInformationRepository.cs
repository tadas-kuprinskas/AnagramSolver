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
    public class SearchInformationRepository : ISearchInformationRepository
    {
        private readonly SqlConnection _sqlConnection;
        private readonly Settings _options;

        public SearchInformationRepository(IOptions<Settings> options)
        {
            _options = options.Value;
            _sqlConnection = new SqlConnection(_options.ConnectionString);
        }

        public void AddSearchInformation(SearchInformation searchInformation)
        {
            _sqlConnection.Open();
            var insertQuery = "Insert into SearchInformation (UserIp, SearchTime, SearchedWord, Anagrams) " +
                "VALUES (@UserIp, @SearchTime, @SearchedWord, @Anagrams)";
            SqlCommand command = new(insertQuery, _sqlConnection);

            command.Parameters.Add(new SqlParameter("@UserIp", searchInformation.UserIp));
            command.Parameters.Add(new SqlParameter("@SearchTime", searchInformation.SearchTime));
            command.Parameters.Add(new SqlParameter("@SearchedWord", searchInformation.SearchedWord));
            command.Parameters.Add(new SqlParameter("@Anagrams", searchInformation.Anagram));
            command.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public IEnumerable<SearchInformation> ReturnSearchInformation()
        {
            _sqlConnection.Open();

            var insertQuery = "Select * from SearchInformation";

            List<SearchInformation> searchInformation = new();

            using (SqlCommand cmd = new(insertQuery, _sqlConnection))
            {
                SqlDataReader dataReader = cmd.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        searchInformation.Add(
                            new SearchInformation()
                            {
                                UserIp = dataReader["UserIp"].ToString(),
                                SearchTime = DateTime.Parse(dataReader["SearchTime"].ToString()),
                                SearchedWord = dataReader["SearchedWord"].ToString(),
                                Anagram = dataReader["Anagrams"].ToString()
                            });
                    }
                }
                dataReader.Close();
            }
            _sqlConnection.Close();
            return searchInformation;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
