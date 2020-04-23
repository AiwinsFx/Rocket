
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Grid;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Tab
{
    public class RocketTabsTagHelper : RocketTagHelper<RocketTabsTagHelper, RocketTabsTagHelperService>
    {
        public string Name { get; set; }

        public TabStyle TabStyle { get; set; } = TabStyle.Tab;

        public ColumnSize VerticalHeaderSize { get; set; } = ColumnSize._3;

        public RocketTabsTagHelper(RocketTabsTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
