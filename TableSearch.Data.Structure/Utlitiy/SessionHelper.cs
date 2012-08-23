using System;
using System.Configuration;
using System.Runtime.Caching;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Structure.Utlitiy
{
    public class SessionHelper
    {
        #region Fields

        private const string MainContextKey = "SessionFactory";

        #endregion
        
        #region SupportMethods

        private static ISessionFactory InitializeSessionFactory()
        {
            var connection = ConfigurationManager.ConnectionStrings["TableSearchDev"].ConnectionString;
            return
                Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008
                                  .ConnectionString(connection)
                                  .ShowSql())
                    .Mappings(mappingConfiguration =>
                              mappingConfiguration.FluentMappings
                                  .AddFromAssemblyOf<ColumnEntity>())
                    .ExposeConfiguration(configuration =>
                        new SchemaExport(configuration).Create(false, false))
                            .BuildSessionFactory();
        }

        #endregion

        #region Methods

        public static ISession CreateASession()
        {
            return SessionFactory.OpenSession();
        }

        #endregion

        #region Properties

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (!MemoryCache.Default.Contains(MainContextKey))
                {
                    var offsetForRemval = new DateTimeOffset(DateTime.Now.AddMinutes(30));
                    var itemPolicy = new CacheItemPolicy { AbsoluteExpiration = offsetForRemval };
                    MemoryCache.Default.Add(MainContextKey, InitializeSessionFactory(), itemPolicy);
                }

                return (ISessionFactory)MemoryCache.Default[MainContextKey];
            }
        }  

        #endregion
    }
}