using System.Collections.Generic;

namespace TableSearch.Data.Structure.Entity
{
    public class TableEntity : IIdModel
    {
        public virtual int Id { get; set; }

        public virtual string DatabaseName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual string SchemaName { get; set; }

        public virtual IList<ColumnEntity> Columns { get; set; }
    }
}