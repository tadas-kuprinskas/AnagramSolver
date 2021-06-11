using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
        private readonly IClientService _clientService;
        private readonly ISearchInformationRepository _searchInformationRepository;

        public SearchInformationService(IClientService clientService, ISearchInformationRepository searchInformationRepository)
        {
            _clientService = clientService;
            _searchInformationRepository = searchInformationRepository;
        }

        private SearchInformation GetSearchInformation(List<Word> uniqueAnagrams, string myWord)
        {
            var userIp = _clientService.GetUserIP();
            var searchTime = DateTime.Now;

            var anagrams = uniqueAnagrams.Select(w => w.Value);
            var anagramsToString = anagrams.Aggregate((current, next) => current + ", " + next);

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
    }
}
