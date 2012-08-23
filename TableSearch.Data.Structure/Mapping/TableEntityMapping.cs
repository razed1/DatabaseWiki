using FluentNHibernate.Mapping;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Structure.Mapping
{
    public class TableEntityMapping : ClassMap<TableEntity>
    {
        public TableEntityMapping()
        {
            Id(x => x.Id);
            
            Map(x => x.DatabaseName)
                .Length(200)
                .Not.Nullable();

            Map(x => x.Description)
                .Length(9001)
                .Nullable();

            Map(x => x.Name)
                .Length(200)
                .Not.Nullable();

            Map(x => x.SchemaName)
                .Length(200)
                .Not.Nullable();

            HasMany(x => x.Columns)
                .KeyColumn("ParentTableId");

            Table("WikiTables");
        }
    }
}