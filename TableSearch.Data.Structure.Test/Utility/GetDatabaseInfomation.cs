using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Utlitiy;


namespace TableSearch.Data.Structure.Test.Utility
{
    [TestFixture]
    public class GetDatabaseInfomation
    {
        #region Support Methods

        private static IEnumerable<string> RetrieveAllDatabaseNames()
        {
            const string databaseQuery = "SELECT Name FROM sys.databases";

            return
                RunQuery(databaseQuery)
                    .Tables[0].Rows.Cast<DataRow>()
                    .Select(x => x["name"].ToString())
                    .ToList();

        }

        private static DataSet RunQuery(string databaseQuery)
        {
            var connection = ConfigurationManager.ConnectionStrings["TableSearchDev"].ConnectionString;
            var dataAdapter = new SqlDataAdapter();

            var serverConnection = new SqlConnection(connection);
            var selectCommand = new SqlCommand(databaseQuery, serverConnection);
            dataAdapter.SelectCommand = selectCommand;

            var dataSetToReturn = new DataSet();
            dataAdapter.Fill(dataSetToReturn);
            dataAdapter.Dispose();
            selectCommand.Dispose();
            selectCommand.Parameters.Clear();

            return dataSetToReturn;
        } 
        
        #endregion

        #region Quasi Workflow Methods

        [Test]
        public void DatabaseNameQueryWorks()
        {
            RetrieveAllDatabaseNames().Any().Should().BeTrue();
        }

        public static IEnumerable<TableEntity> RetrieveTableInfomationByDatabase(IEnumerable<string> databaseNames)
        {
            const string query =
                "USE [{0}] " +
                "SELECT [Schema].Name AS SchemaName,[Table].Name AS TableName,[Table].object_id AS TableId " +
                "FROM Sys.Tables AS [Table]  " +
                "INNER JOIN Sys.Schemas AS [Schema] ON [Schema].schema_id = [Table].schema_id ";

            return
                databaseNames
                    .Select(databaseName =>
                        RunQuery(string.Format(query, databaseName))
                            .Tables[0].Rows.Cast<DataRow>()
                            .Select(x => new TableEntity { SchemaName = x["SchemaName"].ToString(), Name = x["TableName"].ToString(), DatabaseName = databaseName })
                            .ToList()
                    )
                    .Aggregate(new List<TableEntity>(), (inner, outer) =>
                        {
                            outer.AddRange(inner);
                            return outer;
                        });
        }

        [Test]
        public void TablesAreFound()
        {
            var databaseNames = RetrieveAllDatabaseNames();
            var tableInformation = RetrieveTableInfomationByDatabase(databaseNames);

            using (var session = SessionHelper.CreateASession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    foreach (var tableEntity in tableInformation)
                    {
                        session.SaveOrUpdate(tableEntity);
                    }

                    transaction.Commit();
                }
            }
        }

        public static IEnumerable<ColumnEntity> RetrieveColumnInfomationByTable(IEnumerable<TableEntity> tableList)
        {
            const string query = "USE {0} " +
                                 "SELECT [columns].name as [Name], [types].name as [DataTypeName]" +
                                 "FROM sys.Columns [columns] " +
                                 "INNER JOIN sys.tables [tables] ON [tables].object_id = [columns].object_id " +
                                 "INNER JOIN sys.types [types] ON [types].system_type_id = [columns].system_type_id " +
                                 "WHERE[tables].name = '{1}' ";

            return
                tableList
                    .Select(tableName =>
                        RunQuery(string.Format(query, tableName.DatabaseName, tableName.Name))
                            .Tables[0].Rows.Cast<DataRow>()
                                .Select(x => new ColumnEntity { DataType = x["DataTypeName"].ToString(), Name = x["Name"].ToString(), ParentTable = tableName })
                                .ToList()
                        )
                            .Aggregate(new List<ColumnEntity>(), (inner, outer) =>
                            {
                                outer.AddRange(inner);
                                return outer;
                            });
        }

        [Test]
        public void ColumnsAreFound()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var tableList = session.Query<TableEntity>().ToList();
                var columnList = RetrieveColumnInfomationByTable(tableList);

                using (var transaction = session.BeginTransaction())
                {
                    foreach (var columnEntity in columnList)
                    {
                        session.SaveOrUpdate(columnEntity);
                    }

                    transaction.Commit();
                }
            }
        } 

        #endregion
    }
}