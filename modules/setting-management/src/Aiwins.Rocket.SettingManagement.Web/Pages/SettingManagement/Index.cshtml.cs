using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace Aiwins.Rocket.SettingManagement.Web.Pages.SettingManagement
{
    public class IndexModel : RocketPageModel
    {
        public SettingPageCreationContext SettingPageCreationContext { get; private set; }

        protected SettingManagementPageOptions Options { get; }

        public IndexModel(IOptions<SettingManagementPageOptions> options)
        {
            Options = options.Value;
        }

        public virtual async Task OnGetAsync()
        {
            SettingPageCreationContext = new SettingPageCreationContext(ServiceProvider);

            foreach (var contributor in Options.Contributors)
            {
                await contributor.ConfigureAsync(SettingPageCreationContext);
            }
        }

        public virtual Task OnPostAsync()
        {
            return Task.CompletedTask;
        }
    }
}