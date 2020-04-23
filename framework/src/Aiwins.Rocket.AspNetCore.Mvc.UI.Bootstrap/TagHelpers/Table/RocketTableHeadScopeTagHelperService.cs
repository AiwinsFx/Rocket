using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Table
{
    public class RocketTableHeadScopeTagHelperService : RocketTagHelperService<RocketTableHeadScopeTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            SetScope(context, output);
        }

        protected virtual void SetScope(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Scope)
            {
                case RocketThScope.Default:
                    return;
                case RocketThScope.Row:
                    output.Attributes.Add("scope", "row");
                    return;
                case RocketThScope.Column:
                    output.Attributes.Add("scope","col");
                    return;
            }
        }
    }
}