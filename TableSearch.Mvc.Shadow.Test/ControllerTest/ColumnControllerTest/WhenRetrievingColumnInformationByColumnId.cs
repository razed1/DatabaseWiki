using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup;
using FluentAssertions;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.ColumnControllerTest
{
    [TestClass]
    public class WhenRetrievingColumnInformationByColumnId
    {
        private const int ColumnId = -2113;

        #region Fields

        private ColumnControllerShadow _columnControllerShadow;
        private ISession _session;
        private Func<ISession> _sessionMethod;
        private Func<int, ISession, ColumnInformationResult> _queryMethod;
        private ColumnInformationResult _informationResult;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            _columnControllerShadow = new ColumnControllerShadow();

            _session = Substitute.For<ISession>();
            _sessionMethod = () => _session;

            _informationResult = new ColumnInformationResult();
            _queryMethod = (x, session) => _informationResult;
        }

        #endregion

        #region Test Methods
        [TestCategory("BVT"), TestMethod]
        public void AndANewSessionIsCreated()
        {
            var methodGroup = new RetrieveColumnInformationByColumnIdMethodGroup(_queryMethod, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.RetrieveColumnInformationByColumnId(ColumnId, methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheColumnQueryIsCalledCorrectly()
        {
            var methodGroup = new RetrieveColumnInformationByColumnIdMethodGroup((columnId, session) => { if (columnId == ColumnId && session == _session) throw new MethodAccessException(); return null; }, _sessionMethod);
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.RetrieveColumnInformationByColumnId(ColumnId, methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheColumnDoesNotExistSoAnExceptionIsThrown()
        {
            var methodGroup = new RetrieveColumnInformationByColumnIdMethodGroup((x, session) => null, _sessionMethod);
            AssertionExtensions.ShouldThrow<ArgumentException>(() => _columnControllerShadow.RetrieveColumnInformationByColumnId(ColumnId, methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheColumnIsFoundAndItIsReturned()
        {
            var methodGroup = new RetrieveColumnInformationByColumnIdMethodGroup(_queryMethod, _sessionMethod);

            _columnControllerShadow.
                RetrieveColumnInformationByColumnId(ColumnId, methodGroup)
                .Data
                .Should()
                .Be(_informationResult);
        }

        #endregion
    }
}