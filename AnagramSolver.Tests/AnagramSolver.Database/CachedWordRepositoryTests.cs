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
        private Fixture _fixture;
        private string _myWord;
        private string _anagram;

        [SetUp]
        public void Setup()
        {
            _myWord = "stalas";
            _anagram = "Lastas";

            _options = new() { ConnectionString = "Server=.;Database=AnagramSolver;Trusted_Connection=True;", NumberOfAnagrams = 10 };

            SqlConnection _sqlConnection = new(_options.ConnectionString);

            _fixture = new();

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
            var uniqueWord = _fixture.Create<string>(); 
            var id = _cachedWordRepository.AddCachedWord(uniqueWord);

            id.ShouldNotBe(0);
            id.ShouldBeOfType<int>();
        }

        [Test]
        public void GetCachedAnagrams_GivenWord_ReturnsCorrectTypeResult()
        {
            var words = _cachedWordRepository.GetCachedAnagrams(_myWord);

            words.ShouldBeOfType<List<Word>>();
        }

        [Test]
        public void GetCachedAnagrams_GivenWord_ReturnsCorrectAnagram()
        {
            var words = _cachedWordRepository.GetCachedAnagrams(_myWord).Select(w => w.Value);

            words.FirstOrDefault().ShouldBe(_anagram);
        }
    }
}
