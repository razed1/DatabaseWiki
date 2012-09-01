using System;
using FluentAssertions;
using NUnit.Framework;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.TableControllerTest
{
    [TestFixture]
    public class WhenUpdatingATableDescription
    {
        #region Fields

        private const string Description = "description";
        private const int TableId = -321;

        private UpdateTableDescriptionMethodGroup _methodGroup;
        private TableControllerShadow _tableControllerShadow;
        private Func<int, ISession, bool> _tableExists;
        private Action<int, string, ISession> _updateMethod;
        private ISession _session;
        private Func<ISession> _sessionMethod;

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {
            _tableControllerShadow = new TableControllerShadow();
            _tableExists = (x, session) => true;
            _updateMethod = (x, y, z) => { };

            _session = Substitute.For<ISession>();
            _sessionMethod = () => _session;

            _methodGroup = new UpdateTableDescriptionMethodGroup(_tableExists, _updateMethod, _sessionMethod);
        }

        #endregion

        #region Test Methods


        [Test]
        public void AndANewSessionIsCreated()
        {
            _methodGroup = new UpdateTableDescriptionMethodGroup(_tableExists, _updateMethod, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.UpdateTableDescription(TableId, Description, _methodGroup));
        }

        [Test]
        public void AndTheTableQueryIsCalledCorrectly()
        {
            _methodGroup = new UpdateTableDescriptionMethodGroup((x, session) => { if (session == _session) throw new MethodAccessException(); return false; }, _updateMethod, _sessionMethod);
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.UpdateTableDescription(TableId, Description, _methodGroup));
        }

        [Test]
        public void TheTableDoesNotExistSoAnExceptionIsThrown()
        {
            _tableExists = (x, session) => false;
            _methodGroup = new UpdateTableDescriptionMethodGroup(_tableExists, _updateMethod, _sessionMethod);
            AssertionExtensions.ShouldThrow<ArgumentException>(() => _tableControllerShadow.UpdateTableDescription(TableId, Description, _methodGroup));
        }

        [Test]
        public void TheUpdateMethodIsCalledCorrectly()
        {
            _methodGroup = new UpdateTableDescriptionMethodGroup(_tableExists, (x, y, session) => { if (x == TableId && session == _session) throw new MethodAccessException(); }, _sessionMethod);
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.UpdateTableDescription(TableId, Description, _methodGroup));
        }

        [Test]
        public void TheUpdatedIsMadeAndTheTextIsReturned()
        {
            _tableControllerShadow.UpdateTableDescription(TableId, Description, _methodGroup).Data.Should().Be(Description);
        }

        #endregion
    }
}