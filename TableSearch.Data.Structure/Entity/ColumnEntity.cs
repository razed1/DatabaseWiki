using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableSearch.Data.Structure.Entity
{
    public class ColumnEntity : IIdModel
    {
        public virtual int Id { get; set; }

        public virtual string DataType { get; set; }
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }

        public virtual TableEntity ParentTable { get; set; }
    }
}
