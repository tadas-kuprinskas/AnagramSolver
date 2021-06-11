using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AnagramSolver.BusinessLogic.Services
{
    public class AnagramClientService : IClientService
    {
        private readonly Settings _options;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IValidationService _validationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnagramClientService(IOptions<Settings> options, IHttpClientFactory clientFactory, 
            IValidationService validationService, IHttpContextAccessor httpContextAccessor)
        {
            _options = options.Value;
            _clientFactory = clientFactory;
            _validationService = validationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<string>> SendGetAnagramsRequestAsync(string myWord)
        {
            _validationService.ValidateInputLength(myWord);

            var request = new HttpRequestMessage(HttpMethod.Get, _options.WordUri + myWord);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            var anagrams = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                anagrams = JsonSerializer.Deserialize<List<string>>(contentString);
            }
            return anagrams;
        }

        public string GetUserIP()
        {
            string ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return ip;
        }
    }
}
