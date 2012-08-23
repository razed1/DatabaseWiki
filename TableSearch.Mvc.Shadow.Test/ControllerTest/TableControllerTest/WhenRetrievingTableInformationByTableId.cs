using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NSubstitute;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup;
using TableSearch.Shared.Test;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.Test.ControllerTest.TableControllerTest
{
    [TestClass]
    public class WhenRetrievingTableInformationByTableId
    {
        #region Fields

        private const int TableId = -231;

        private TableControllerShadow _tableControllerShadow;
        private Func<int, ISession, TableInformationResult> _searchForTable;
        private TableInformationResult _tableInformationResult;
        private Func<int, ISession, IEnumerable<ColumnItemResult>> _queryForColumns;
        private RetrieveTableInformationMethodGroup _methodGroup;
        private ISession _session;
        private Func<ISession> _sessionMethod;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            _tableControllerShadow = new TableControllerShadow();

            _session = Substitute.For<ISession>();
            _sessionMethod = () => _session;

            _queryForColumns = (x, session) => new List<ColumnItemResult>();
            _searchForTable = (tableId, session) => _tableInformationResult;
            _methodGroup = new RetrieveTableInformationMethodGroup(_searchForTable, _queryForColumns, _sessionMethod);
            _tableInformationResult = new TableInformationResult {Id = RandomTool.RandomInt32()};

        }

        #endregion

        #region Test Methods

        [TestCategory("BVT"), TestMethod]
        public void AndANewSessionIsCreated()
        {
            _methodGroup = new RetrieveTableInformationMethodGroup(_searchForTable, _queryForColumns, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.RetrieveTableInformationByTableId(TableId, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void AndTheTableQueryMethodIsCalledCorrectly()
        {
            _searchForTable = (x, session) => { if (session == _session) throw new MethodAccessException(); return null; };
            _methodGroup = new RetrieveTableInformationMethodGroup(_searchForTable, _queryForColumns, () => { throw new MethodAccessException(); });
            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.RetrieveTableInformationByTableId(TableId, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheTableDoesNotExistSoAnExceptionIsThrown()
        {
            _methodGroup = new RetrieveTableInformationMethodGroup((tableId, session) => null, _queryForColumns, _sessionMethod);
            AssertionExtensions.ShouldThrow<ArgumentException>(() => _tableControllerShadow.RetrieveTableInformationByTableId(TableId, _methodGroup));
        }
      
        [TestCategory("BVT"), TestMethod]
        public void TheTableExistsSoTheResultIsReturned()
        {
            _tableControllerShadow
                .RetrieveTableInformationByTableId(TableId, _methodGroup)
                .Data
                .Should()
                .Be(_tableInformationResult);
        }

        [TestCategory("BVT"), TestMethod]
        public void AndTheColumnQueryIsCalledCorrectly()
        {
            _queryForColumns = (text, session) => { if (session == _session) throw new MethodAccessException(); return null; };
            _methodGroup = new RetrieveTableInformationMethodGroup(_searchForTable, _queryForColumns, _sessionMethod);

            AssertionExtensions.ShouldThrow<MethodAccessException>(() => _tableControllerShadow.RetrieveTableInformationByTableId(TableId, _methodGroup));
        }

        [TestCategory("BVT"), TestMethod]
        public void TheColumnsForTheTableAreQueriedAndSetOnTheResult()
        {
            var columnList = new List<ColumnItemResult> {new ColumnItemResult(), new ColumnItemResult()};

            Func<int, ISession, IEnumerable<ColumnItemResult>> queryForColumns = (x, session) =>
                {
                    if (x != _tableInformationResult.Id)
                    {
                        throw new ArgumentException();
                    }

                    return columnList;
                };

            _methodGroup = new RetrieveTableInformationMethodGroup(_searchForTable, queryForColumns, _sessionMethod);

            ((TableInformationResult)
            _tableControllerShadow
                .RetrieveTableInformationByTableId(TableId, _methodGroup)
                .Data)
                .ColumnList
                .Should()
                .BeEquivalentTo(columnList);
        }

        #endregion
    }
}