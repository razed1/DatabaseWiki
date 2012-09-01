using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Utlitiy;


namespace TableSearch.Data.Structure.Test.MappingTest
{
    [TestFixture]
    public class ColumnMappingTest : MappingTestBase
    {
        #region Test Methods

        [Test]
        public void ItemCreated()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var name = Guid.NewGuid().ToString();

                var itemToSave = (new ColumnEntityCreator()).Create(session, CleanUp, name: name);

                session.Query<ColumnEntity>()
                    .Any(item => item.Name == itemToSave.Name)
                    .Should()
                    .BeTrue();
            }
        }

        #endregion
    }
}