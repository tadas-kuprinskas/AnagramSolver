using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class SearchInformationService : ISearchInformationService
    {
        private readonly ISearchInformationRepository _searchInformationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchInformationService(ISearchInformationRepository searchInformationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _searchInformationRepository = searchInformationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private SearchInformation GetSearchInformation(List<Word> uniqueAnagrams, string myWord)
        {
            var userIp = GetUserIP();
            var searchTime = DateTime.Now;

            string anagramsToString;
            IEnumerable<string> anagrams;

            if (uniqueAnagrams.Any())
            {
                anagrams = uniqueAnagrams.Select(w => w.Value);
                anagramsToString = anagrams.Aggregate((current, next) => current + ", " + next);
            }
            else
            {
                anagramsToString = string.Empty;
            }

            SearchInformation searchInformation = new()
            {
                SearchTime = searchTime,
                UserIp = userIp,
                Anagram = anagramsToString,
                SearchedWord = myWord
            };

            return searchInformation;
        }

        public void RecordSearchInformation(List<Word> uniqueAnagrams, string myWord)
        {
            var information = GetSearchInformation(uniqueAnagrams, myWord);

            _searchInformationRepository.AddSearchInformation(information);
        }

        public IEnumerable<SearchInformation> ReturnAllSearches()
        {
            return _searchInformationRepository.ReturnSearchInformation();
        }

        public string GetUserIP()
        {
            string ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return ip;
        }
    }
}
