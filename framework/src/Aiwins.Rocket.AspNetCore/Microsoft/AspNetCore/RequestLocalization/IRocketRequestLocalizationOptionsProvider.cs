﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.RequestLocalization {
    public interface IRocketRequestLocalizationOptionsProvider {
        void InitLocalizationOptions (Action<RequestLocalizationOptions> optionsAction = null);

        RequestLocalizationOptions GetLocalizationOptions ();

        Task<RequestLocalizationOptions> GetLocalizationOptionsAsync ();
    }
}