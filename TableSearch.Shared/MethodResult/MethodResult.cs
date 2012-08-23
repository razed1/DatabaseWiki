using System.Collections.Generic;
using System.Linq;
using TableSearch.Shared.Functional;
using TableSearch.Shared.MethodResult.Message;

namespace TableSearch.Shared.MethodResult
{
    public class MethodResult<T>
    {
        #region Contructors

        public MethodResult()
        {
            _messages = new List<MessageItem>();
            Success = true;
        }

        public MethodResult(IEnumerable<MessageItem> messages, T value)
        {
            _messages = messages;
            ReturnValue = value;
            Success = !(_messages.Any(message => message.Category == MessageCategory.Error));
        }

        #endregion

        #region Fields

        private readonly IEnumerable<MessageItem> _messages;

        #endregion

        #region Methods

        public MethodResult<T> AddErrorMessage(string message)
        {
            GuardClause.IfIsNullOrEmptyThrowArgumentException(message, "MethodResult.AddMessage: messageItem is null.");

            return AddMessage(new MessageItem(message, MessageCategory.Error));
        }

        public MethodResult<T> AddInfoMessage(string message)
        {
            GuardClause.IfIsNullOrEmptyThrowArgumentException(message, "MethodResult.AddMessage: messageItem is null.");

            return AddMessage(new MessageItem(message, MessageCategory.Information));
        }

        public MethodResult<T> AddMessage(MessageItem messageItem)
        {
            GuardClause.IfNullThrowArgumentException(messageItem, "MethodResult.AddMessage: messageItem is null.");
            var newList = GetUnionWithoutRepeat(Messages, new List<MessageItem> { new MessageItem(messageItem.Message, messageItem.Category) });

            return new MethodResult<T>(newList, ReturnValue);
        }

        public MethodResult<T> AddWarningMessage(string message)
        {
            GuardClause.IfIsNullOrEmptyThrowArgumentException(message, "MethodResult.AddMessage: messageItem is null.");

            return AddMessage(new MessageItem(message, MessageCategory.Warning));
        }

        private IEnumerable<MessageItem> GetUnionWithoutRepeat(IEnumerable<MessageItem> firstList, IEnumerable<MessageItem> secondList)
        {
            var differenceList =
             secondList
               .Where(mergedMessage => !firstList
                 .Any(indigenousMessage => indigenousMessage.Category == mergedMessage.Category && indigenousMessage.Message == mergedMessage.Message));

            return firstList.Union(differenceList);
        }

        public MethodResult<T> Merge(MethodResult<T> resultToMerge)
        {
            GuardClause.IfNullThrowArgumentException(resultToMerge, "MethodResult.Merge: resultToMerge is null.");
            var newList = GetUnionWithoutRepeat(Messages, resultToMerge.Messages);

            return new MethodResult<T>(newList, ReturnValue);
        }

        public MethodResult<T> SetValue(T valueToSet)
        {
            return new MethodResult<T>(Messages, valueToSet);
        }

        #endregion

        #region Properties

        public IEnumerable<MessageItem> Messages
        {
            get { return _messages.ToList(); }
        }

        public T ReturnValue { get; private set; }

        public bool Success { get; set; }

        #endregion


    }
}