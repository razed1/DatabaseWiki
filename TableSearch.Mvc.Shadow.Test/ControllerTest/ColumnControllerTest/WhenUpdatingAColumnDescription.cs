using System;
using NUnit.Framework;
using FluentAssertions;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.ColumnControllerTest
{
    [TestFixture]
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

        [SetUp]
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


        [Test]
        public void AndANewSessionIsCreated()
        {
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [Test]
        public void AndTheColumnQueryIsCalledCorrectly()
        {
            _columnExists = (id, session) => { if (id == ColumnId && session == _session) throw new MethodAccessException(); return false; };
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
        }

        [Test]
        public void TheColumnDoesNotExistSoAnExceptionIsThrown()
        {
            _columnExists = (x, session) => false;
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);

            AssertionExtensions.ShouldThrow<ArgumentException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [Test]
        public void TheUpdateMethodIsCalledCorrectly()
        {
            _updateMethod = (columnId, y, session) => { if (columnId == ColumnId && session == _session) throw new MethodAccessException(); };

            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup));
        }

        [Test]
        public void TheUpdatedIsMadeAndTheTextIsReturned()
        {
            _methodGroup = new UpdateColumnDescriptionMethodGroup(_columnExists, _updateMethod, _sessionMethod);
            _columnControllerShadow.UpdateColumnDescription(ColumnId, Description, _methodGroup).Data.Should().Be(Description);
        }

        #endregion
    }
}