using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TableSearch.Service.Validation.Error;
using TableSearch.Service.Validation.Restriction;
using TableSearch.Shared.Test;

namespace TableSearch.Service.Validation.Test.SearchValidatorTest
{
    [TestFixture]
    public class WhenValidatingSearchText
    {
        #region Fields

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {

        }

        #endregion

        #region Test Methods

        [Test]
        public void AndTheSearchTextIsNotLongEnoughAUnsuccessfulResultIsReturned()
        {
            SearchValidator.SearchValidator
                .ValidateThatSearchTextIsLongEnough(RandomTool.RandomString(SearchRestriction.MinimumSearchTestLength - 1))
                .Success
                .Should()
                .BeFalse();
        }

        [Test]
        public void AndTheSearchTextIsNotLongEnoughAUnsuccessfulMessageIsReturned()
        {
            SearchValidator.SearchValidator
                .ValidateThatSearchTextIsLongEnough(
                    RandomTool.RandomString(SearchRestriction.MinimumSearchTestLength - 1))
                .Messages
                .Any(x => x.Message == string.Format(SearchValidationError.SearchTextIsTooShort, SearchRestriction.MinimumSearchTestLength))
                .Should()
                .BeTrue();
        }

        #endregion
    }
}