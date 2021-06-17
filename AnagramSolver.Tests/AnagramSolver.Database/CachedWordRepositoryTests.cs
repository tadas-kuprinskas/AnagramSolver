using AnagramSolver.BusinessLogic.Utilities;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Repository;
using AutoFixture;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.Database
{
    [TestFixture]
    public class CachedWordRepositoryTests
    {
        private CachedWordRepository _cachedWordRepository;
        private Settings _options;      
        private string _myWord;

        [SetUp]
        public void Setup()
        {
            _myWord = "stalas";

            _options = new() { ConnectionString = "Server=.;Database=Anagram_Solver;Trusted_Connection=True;", NumberOfAnagrams = 10 };

            SqlConnection _sqlConnection = new(_options.ConnectionString);

            var mockOptions = new Mock<IOptions<Settings>>();
            mockOptions.Setup(ap => ap.Value).Returns(_options);

            _cachedWordRepository = new(mockOptions.Object);
        }

        [Test]
        public void SearchCachedWord_GivenWord_ReturnsCorrectTypeResult()
        {
            var result = _cachedWordRepository.SearchCachedWord(_myWord);

            result.ShouldBeOfType<CachedWord>();
        }

        [Test]
        public void AddCachedWord_GivenWord_ReturnsCorrectTypeResultAsOutput()
        {
            CachedWord uniqueWord = new()
            {
                Id = 1,
                Value = "sula"
            };

            var id = _cachedWordRepository.AddCachedWord(uniqueWord);

            id.ShouldBeOfType<CachedWord>();
        }

        [Test]
        public void GetCachedAnagrams_GivenWord_ReturnsCorrectTypeResult()
        {
            var words = _cachedWordRepository.GetCachedAnagrams(_myWord);

            words.ShouldBeOfType<List<Word>>();
        }
    }
}
