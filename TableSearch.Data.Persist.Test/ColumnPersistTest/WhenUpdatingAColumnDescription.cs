﻿using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Persist.Test.ColumnPersistTest
{
    [TestClass]
    public class WhenUpdatingAColumnDescription : MappingTestBase
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
        public void AndTheDescriptionIsUpdated()
        {
            var oldDescription = RandomTool.RandomString(30);
            var newDescription = RandomTool.RandomString(30);

            using (var sesison = SessionHelper.CreateASession())
            {
                var column = new ColumnEntityCreator().Create(sesison, CleanUp, description: oldDescription);

                ColumnPersist.UpdateColumnDescription(column.Id, newDescription, sesison);
                sesison.Query<ColumnEntity>().First(x => x.Id == column.Id).Description.Should().Be(newDescription);
            }
        }

        #endregion
    }
}