using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramClientService : IClientService
    {
        private readonly Settings _options;
        private readonly HttpClient _client;
        private readonly IValidationService _validationService;

        public AnagramClientService(IOptions<Settings> options, IValidationService validationService)
        {
            _options = options.Value;
            _client = new();
            _validationService = validationService;
        }

        public async Task<IEnumerable<string>> SendGetAnagramsRequestAsync(string myWord)
        {
            _validationService.ValidateInputLength(myWord);

            var httpResponse = await _client.GetAsync(_options.WordUri + myWord);

            var anagrams = new List<string>();

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                anagrams = JsonConvert.DeserializeObject<List<string>>(contentString);
            }
            return anagrams;
        }
    }
}
