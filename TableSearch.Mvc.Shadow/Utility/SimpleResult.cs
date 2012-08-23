using System.Collections.Generic;
using System.Linq;
using TableSearch.Shared.MethodResult;

namespace TableSearch.Mvc.Shadow.Utility
{
    public class SimpleResult<T>
    {
        #region Constructors

        public SimpleResult(MethodResult<T> resultToConsume)
        {
            Messages = resultToConsume.Messages.Select(messsage => messsage.Message);
            Success = resultToConsume.Success;
            Value = resultToConsume.ReturnValue;
        }

        #endregion

        #region Fields

        #endregion

        #region Methods

        #endregion

        #region Properties

        public IEnumerable<string> Messages { get; set; }

        public bool Success { get; private set; }

        public string RedirectUrl { get; private set; }

        public T Value { get; private set; }

        #endregion
    }
}