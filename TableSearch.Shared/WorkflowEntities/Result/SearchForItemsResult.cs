namespace TableSearch.Shared.WorkflowEntities.Result
{
    public class SearchForItemsResult
    {
        public SearchForItemsResult(int id, string databaseName, string schemaName, string tableName)
        {
            Id = id;
            DatabaseName = databaseName;
            SchemaName = schemaName;
            TableName = tableName;
        }

        public string DatabaseName { get; private set; }
        public int Id { get; private set; } 
        public object SchemaName { get; private set; }
        public string TableName { get; private set; }
    }
}