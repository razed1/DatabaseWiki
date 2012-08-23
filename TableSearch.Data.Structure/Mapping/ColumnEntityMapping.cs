using FluentNHibernate.Mapping;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Structure.Mapping
{
    public class ColumnEntityMapping : ClassMap<ColumnEntity>
    {
        public ColumnEntityMapping()
        {
            Id(x => x.Id);

            Map(x => x.DataType)
               .Length(200)
               .Not.Nullable();

            Map(x => x.Description)
                .Length(9001)
                .Nullable();

            Map(x => x.Name)
                .Length(200)
                .Not.Nullable();

            References(x => x.ParentTable)
                .Column("ParentTableId")
                .Not.Nullable();

            Table("WikiColumns");
        }
    }
}