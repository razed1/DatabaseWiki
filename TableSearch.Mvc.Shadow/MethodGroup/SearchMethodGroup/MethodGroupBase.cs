using System;
using NHibernate;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup
{
    public class MethodGroupBase
    {
        #region Constructors

        public MethodGroupBase()
        {
            SessionMethod = SessionHelper.CreateASession;
        }

        public MethodGroupBase(Func<ISession> sessionMethod)
        {
            SessionMethod = sessionMethod;
        }

        #endregion

        #region Properties

        public Func<ISession> SessionMethod { get; private set; } 

        #endregion
    }
}