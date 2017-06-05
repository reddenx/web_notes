using SMT.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebNotes.Persistence.Repositories;

namespace WebNotesSite.Framework
{
    public class SiteConfiguration : ConfigurationBase, IRepositoryConfiguration
    {
        [AppSettings("jquery_cdn")]
        public readonly string JQueryCdnUrl;

        [AppSettings("vuejs_cdn")]
        public readonly string VueJsCdnUrl;

        [AppSettings("typecache_storage_directory")]
        public readonly string TypecacheStorageDirectory;

        [ConnectionString("accounts_connection")]
        public string AccountSqlConnectionString { get; private set; }

        [ConnectionString("notes_connection")]
        public string NoteSqlConnectionString { get; private set; }
    }
}