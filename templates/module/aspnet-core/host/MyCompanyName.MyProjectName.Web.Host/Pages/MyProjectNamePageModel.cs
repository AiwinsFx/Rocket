﻿using MyCompanyName.MyProjectName.Localization;
using Aiwins.Rocket.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Pages
{
    public abstract class MyProjectNamePageModel : RocketPageModel
    {
        protected MyProjectNamePageModel()
        {
            LocalizationResourceType = typeof(MyProjectNameResource);
        }
    }
}