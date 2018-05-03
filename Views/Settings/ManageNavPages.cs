using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Razor_VS_Code_test.Views.Settings
{
    public static class ManageNavPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string ManageActions => "ManageActions";

        public static string ManageTags => "ManageTags";

        public static string ManageImages => "ManageImages";

        public static string ManageSlider => "ManageSlider";


        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string ManageActionsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageActions);

        public static string ManagetTagsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageTags);

        public static string ManageImagesNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageImages);

        public static string ManageSliderNavClass(ViewContext viewContext) => PageNavClass(viewContext, ManageSlider);
    
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}