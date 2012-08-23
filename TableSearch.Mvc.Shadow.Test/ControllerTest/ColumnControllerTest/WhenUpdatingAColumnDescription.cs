using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.ColumnControllerTest
{
    [TestClass]
    public class WhenUpdatingAColumnDescription
    {
        private const int ColumnId = -123;
        private const string Description = "";

        #region Fields

        private ColumnControllerShadow _columnControllerShadow;
        private Func<int, ISession, bool> _columnExists;
        private UpdateColumnDescriptionMethodGroup _methodGroup;
        private Action<int, string, ISession> _updateMethod;
        private ISession _session;
        private Func<ISession> _sessionMethod;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            _columnExists = (x, session) => true;
            _updateMethod = (x, y, z) => { };

            _session = Substitute.For<ISession>();
            _sessionMethod = () => _session;
            _columnControllerShadow = new ColumnControllerShadow();
        }

        #endregion

        #region Test Methods


        [TestCategory("BVT"), TestMethod]
        public void AndANewSessionIsCreated()
        {
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void AndTheColumnQueryIsCalledCorrectly()
        {
            _columnExists = (id, session) => { if (id == ColumnId && session == _session) throw new MethodAccessException(); return false; };
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
        }

        [TestCategory("BVT"), TestMethod]
        public void TheColumnDoesNotExistSoAnExceptionIsThrown()
        {
            _columnExists = (x, session) => false;
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);

            AssertionExtensions.ShouldThrow<ArgumentException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheUpdateMethodIsCalledCorrectly()
        {
            _updateMethod = (columnId, y, session) => { if (columnId == ColumnId && session == _session) throw new MethodAccessException(); };

            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheUpdatedIsMadeAndTheTextIsReturned()
        {
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
            _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup).Data.Should().Be(Description);
        }

        #endregion
    }
}