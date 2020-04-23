using System.Collections.Generic;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Bundling;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.Codemirror;
using Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.HighlightJs;
using Aiwins.Rocket.Modularity;

namespace Aiwins.Rocket.AspNetCore.Mvc.UI.Packages.TuiEditor
{
    [DependsOn(
        typeof(CodemirrorStyleContributor),
        typeof(HighlightJsStyleContributor)
    )]
    public class TuiEditorStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/libs/tui-editor/tui-editor.min.css");
            context.Files.AddIfNotContains("/libs/tui-editor/tui-editor-contents.min.css");
        }
    }
}