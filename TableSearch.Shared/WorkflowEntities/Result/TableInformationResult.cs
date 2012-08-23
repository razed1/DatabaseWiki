using System.Collections.Generic;

namespace TableSearch.Shared.WorkflowEntities.Result
{
    public class TableInformationResult
    {
        public IEnumerable<ColumnItemResult> ColumnList { get; set; }
        public string DatabaseName { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
    }
}