using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;
using TableSearch.Mvc.Shadow.Utility;
using TableSearch.Shared.MethodResult;
using TableSearch.Shared.Test;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.SearchControllerTest
{
    [TestClass]
    public class WhenSearchingForTables
    {
        #region Fields

        private const int ListCount = 5;

        private SearchControllerShadow _searchController;
        private Func<string, ISession, IList<SearchForItemsResult>> _searchQueryMethod;
        private string _searchString;
        private Func<string, MethodResult<bool>> _searchTextValidation;
        private IList<SearchForItemsResult> _resultList;
        private SearchForItemsMethodGroup _searchForItemsMethodGroup;
        private Func<ISession> _sessionMethod;
        private ISession _session;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            _searchString = RandomTool.RandomString();

            _searchController = new SearchControllerShadow();
            _searchTextValidation = (text) => new MethodResult<bool>();

            _resultList = Enumerable.Range(0, ListCount).Select(item => new SearchForItemsResult(ListCount, RandomTool.RandomString(), RandomTool.RandomString(), RandomTool.RandomString())).ToList();
            
            _session = Substitute.For<ISession>();
            _sessionMethod = () => _session;

            _searchQueryMethod = (text, session) => _resultList;
            
            _searchForItemsMethodGroup = new SearchForItemsMethodGroup(_searchTextValidation, _searchQueryMethod, _sessionMethod);
        }

        #endregion

        #region Test Methods

        [TestCategory("BVT"), TestMethod]
        public void AndTheSearchTextIsTooShortAnUnsuccessfulRestultIsReturned()
        {
            _searchTextValidation = (text) => new MethodResult<bool>().AddErrorMessage("error");
            _searchForItemsMethodGroup = new SearchForItemsMethodGroup(_searchTextValidation, _searchQueryMethod, _sessionMethod);

            ((SimpleResult<IList<SearchForItemsResult>>)
                _searchController
                    .SearchForItems(_searchString, _searchForItemsMethodGroup)
                    .Data)
                    .Success
                    .Should()
                    .BeFalse();
        }

        [TestCategory("BVT"), TestMethod]
        public void AndANewSessionIsCreated()
        {
            _searchForItemsMethodGroup = new SearchForItemsMethodGroup(_searchTextValidation, _searchQueryMethod, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _searchController.SearchForItems(_searchString, _searchForItemsMethodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void AndTheSearchMethodIsCalledCorrectly()
        {
            _searchQueryMethod = (text, session) => { if (session == _session) throw new MethodAccessException(); return null; };
            _searchForItemsMethodGroup = new SearchForItemsMethodGroup(_searchTextValidation, _searchQueryMethod, _sessionMethod);

            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _searchController.SearchForItems(_searchString, _searchForItemsMethodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void AndTheSearchResultReturnsAnEmptyListSoTheEmptyListIsReturned()
        {
            _searchQueryMethod = (text, session) => new List<SearchForItemsResult>();
            _searchForItemsMethodGroup = new SearchForItemsMethodGroup(_searchTextValidation, _searchQueryMethod, _sessionMethod);

            ((SimpleResult<IList<SearchForItemsResult>>)
                 _searchController
                     .SearchForItems(_searchString, _searchForItemsMethodGroup)
                     .Data)
                    .Value
                    .Any()
                    .Should()
                    .BeFalse();
        }

        [TestCategory("BVT"), TestMethod]
        public void AndThereAreResultsThatAreAttachedToTheResult()
        {
            ((SimpleResult<IList<SearchForItemsResult>>)
                _searchController
                    .SearchForItems(_searchString, _searchForItemsMethodGroup)
                    .Data)
                .Value
                .Count
                .Should()
                .Be(ListCount);
        }

        #endregion
    }
}