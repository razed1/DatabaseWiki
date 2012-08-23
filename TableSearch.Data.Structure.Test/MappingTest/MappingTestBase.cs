﻿using TableSearch.Data.Structure.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableSearch.Data.Structure.Test.Utility;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Structure.Test.MappingTest
{
    [TestClass]
    public class MappingTestBase
    {
        #region Fields
        
        private CleanUp _cleanUp;

        #endregion

        #region Constructors

        public MappingTestBase()
        {
            _cleanUp = new CleanUp();
        } 

        #endregion

        #region Test Hooks

        [TestCleanup]
        public void TearDown()
        {
            using (var session = SessionHelper.CreateASession())
            {
                _cleanUp.DeleteAll(session);
            }
        } 

        #endregion

        #region Methods

        public void AddForDeletion<T>(T toDelete) where T : IIdModel
        {
            _cleanUp.AddForDeletion(toDelete);
        } 

        #endregion

        #region Properties
        
        public CleanUp CleanUp
        {
            get { return _cleanUp ?? (_cleanUp = new CleanUp()); }
        } 

        #endregion
    }
}