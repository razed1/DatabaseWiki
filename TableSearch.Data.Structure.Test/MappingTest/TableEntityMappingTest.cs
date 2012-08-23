using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Structure.Test.MappingTest
{
    [TestClass]
    public class TableEntityMappingTest : MappingTestBase
    {
        #region Test Methods

        [TestMethod]
        public void ItemCreated()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var name = Guid.NewGuid().ToString();

                var itemToSave = (new TableEntityCreator()).Create(session, CleanUp, name: name);

                session.Query<TableEntity>()
                    .Any(item => item.Name == itemToSave.Name)
                    .Should()
                    .BeTrue();
            }
        }

        #endregion
    }
}