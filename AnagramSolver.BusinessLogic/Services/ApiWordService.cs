using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class ApiWordService : IApiWordService
    {
        private readonly IWordRepository _wordRepository;
        public const string wordUri = "https://localhost:44379/Word/Anagrams?myWord=";

        public ApiWordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IEnumerable<Word> GetPaginatedWords(int currentPage, int pageSize)
        {
            PageModel pageModel = new();
            var words = _wordRepository.ReadAndGetDictionary().Values.ToList().SelectMany(w => w).ToList();

            int count = words.Count;
            pageModel.PageSize = pageSize < 1 ? pageModel.PageSize : pageSize;
            pageModel.TotalPages = (int)Math.Ceiling(count / (double)pageModel.PageSize);

            pageModel.PageNumber = currentPage > pageModel.TotalPages ? pageModel.TotalPages : currentPage;
            var items = words.Skip((pageModel.PageNumber - 1) * pageModel.PageSize).Take(pageModel.PageSize).ToList();

            return items;
        }

        public async Task<IEnumerable<Word>> SendGetAnagramsRequest(string myWord)
        {
            HttpClient client = new();
            var httpResponse = await client.GetAsync(wordUri + myWord);

            var anagrams = new List<Word>();

            if (httpResponse.IsSuccessStatusCode)
            {
                var contentString = await httpResponse.Content.ReadAsStringAsync();
                anagrams = JsonConvert.DeserializeObject<List<Word>>(contentString);
            }
            return anagrams;
        }
    }
}
