namespace TableSearch.Shared.MethodResult.Message
{
    public class MessageItem
    {
        public MessageItem(string message, MessageCategory category)
        {
            Message = message;
            Category = category;
        }

        public string Message { get; private set; }
        public MessageCategory Category { get; private set; }
    }
}