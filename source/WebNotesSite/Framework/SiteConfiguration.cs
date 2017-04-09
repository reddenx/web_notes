using SMT.Utilities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNotesSite.Framework
{
    public class SiteConfiguration : ConfigurationBase
    {
        [AppSettings("jquery_cdn")]
        public readonly string JQueryCdnUrl;

        [AppSettings("vuejs_cdn")]
        public readonly string VueJsCdnUrl;

        [AppSettings("typecache_storage_directory")]
        public readonly string TypecacheStorageDirectory;
    }
}