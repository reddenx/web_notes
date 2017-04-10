using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        private static List<string> IncludedFiles = new List<string>();

        public static MvcHtmlString IncludeJs(this HtmlHelper myHelpyPants, string path)
        {
            IncludedFiles.Add(path);

            return MvcHtmlString.Create($"<script type=\"text/javascript\" src=\"{path}\"></script>");
        }
    }
}