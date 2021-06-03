using AnagramSolver.BusinessLogic.StaticHelpers;
using AnagramSolver.Contracts.Models;
using AutoFixture;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Tests.AnagramSolver.BusinessLogic.StaticHelpers
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void MapToWord_GivenPropertyValues_ReturnsWordWithCorrectProperties()
        {
            var fixture = new Fixture();
            fixture.Customize<Word>(b => b.With(p => p.Value, "sula").With(p => p.OrderedValue, "alsu").With(p => p.PartOfSpeech, "dkt"));
            
            var word = fixture.Create<Word>();

            var returnedWord = Mapping.MapToWord(word.OrderedValue, word.Value, word.PartOfSpeech);

            returnedWord.Value.ShouldBe(word.Value);
        }
    }
}
