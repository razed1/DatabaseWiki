using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Persist.Test.TablePersistTest
{
    [TestFixture]
    public class WhenUpdatingATableDescription : MappingTestBase
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
        public void AndTheDescriptionIsUpdated()
        {
            var oldDescription = RandomTool.RandomString(30);
            var newDescription = RandomTool.RandomString(30);

            using (var sesison = SessionHelper.CreateASession())
            {
                var table = new TableEntityCreator().Create(sesison, CleanUp, description: oldDescription);

                TablePersist.UpdateTableDescription(table.Id, newDescription, sesison);
                sesison.Query<TableEntity>().First(x => x.Id == table.Id).Description.Should().Be(newDescription);
            }
        }

        #endregion
    }
}