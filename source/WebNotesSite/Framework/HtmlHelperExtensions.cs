using System;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using System.Linq;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString IncludeJs(this HtmlHelper myHelpyPants, string path)
        {
            var includedFiles = myHelpyPants.ViewBag.IncludedFiles ?? (myHelpyPants.ViewBag.IncludedFiles = new List<string>());

            if (includedFiles.Contains(path))
            {
                return MvcHtmlString.Empty;
            }

            includedFiles.Add(path);
            return MvcHtmlString.Create($"<script type=\"text/javascript\" src=\"{path}\"></script>");
        }

        public static MvcHtmlString IncludeComponent(this HtmlHelper mrHelper, string componentName)
        {
            var includedComponents = mrHelper.ViewBag.IncludedComponents ?? (mrHelper.ViewBag.IncludedComponents = new List<string>());

            if (includedComponents.Contains(componentName))
            {
                return MvcHtmlString.Empty;
            }

            includedComponents.Add(componentName);
            return mrHelper.Partial($"/Views/Shared/VueComponents/{componentName}.cshtml");
        }

    }
}