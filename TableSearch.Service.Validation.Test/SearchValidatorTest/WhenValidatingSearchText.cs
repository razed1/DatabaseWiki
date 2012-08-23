using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableSearch.Service.Validation.Error;
using TableSearch.Service.Validation.Restriction;
using TableSearch.Shared.Test;

namespace TableSearch.Service.Validation.Test.SearchValidatorTest
{
    [TestClass]
    public class WhenValidatingSearchText
    {
        #region Fields

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {

        }

        #endregion

        #region Test Methods

        [TestCategory("BVT"), TestMethod]
        public void AndTheSearchTextIsNotLongEnoughAUnsuccessfulResultIsReturned()
        {
            SearchValidator.SearchValidator
                .ValidateThatSearchTextIsLongEnough(RandomTool.RandomString(SearchRestriction.MinimumSearchTestLength - 1))
                .Success
                .Should()
                .BeFalse();
        }

        [TestCategory("BVT"), TestMethod]
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