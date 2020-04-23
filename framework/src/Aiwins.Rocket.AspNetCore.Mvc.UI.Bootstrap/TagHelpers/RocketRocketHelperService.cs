using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Aiwins.Rocket.Threading;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers
{
    //TODO: Refactor this class, extract bootstrap functionality!
    public abstract class RocketTagHelperService<TTagHelper> : IRocketTagHelperService<TTagHelper>
        where TTagHelper : TagHelper
    {
        protected const string FormGroupContents = "FormGroupContents";
        protected const string TabItems = "TabItems";
        protected const string AccordionItems = "AccordionItems";
        protected const string BreadcrumbItemsContent = "BreadcrumbItemsContent";
        protected const string CarouselItemsContent = "CarouselItemsContent";
        protected const string TabItemsDataTogglePlaceHolder = "{_data_toggle_Placeholder_}";
        protected const string TabItemNamePlaceHolder = "{_Tab_Tag_Name_Placeholder_}";
        protected const string RocketFormContentPlaceHolder = "{_RocketFormContentPlaceHolder_}";
        protected const string RocketTabItemActivePlaceholder = "{_Tab_Active_Placeholder_}";
        protected const string RocketTabDropdownItemsActivePlaceholder = "{_Tab_DropDown_Items_Placeholder_}";
        protected const string RocketTabItemShowActivePlaceholder = "{_Tab_Show_Active_Placeholder_}";
        protected const string RocketBreadcrumbItemActivePlaceholder = "{_Breadcrumb_Active_Placeholder_}";
        protected const string RocketCarouselItemActivePlaceholder = "{_CarouselItem_Active_Placeholder_}";
        protected const string RocketTabItemSelectedPlaceholder = "{_Tab_Selected_Placeholder_}";
        protected const string RocketAccordionParentIdPlaceholder = "{_Parent_Accordion_Id_}";

        public TTagHelper TagHelper { get; internal set; }

        public virtual int Order { get; }

        public virtual void Init(TagHelperContext context)
        {

        }

        public virtual void Process(TagHelperContext context, TagHelperOutput output)
        {

        }

        public virtual Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            return Task.CompletedTask;
        }
    }
}