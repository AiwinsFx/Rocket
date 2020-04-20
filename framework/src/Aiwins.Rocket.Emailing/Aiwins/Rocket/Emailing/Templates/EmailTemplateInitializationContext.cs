﻿using System;

namespace Aiwins.Rocket.Emailing.Templates
{
    public class EmailTemplateInitializationContext
    {
        public EmailTemplateDefinition EmailTemplateDefinition { get; }

        public IServiceProvider ServiceProvider { get; }

        public EmailTemplateInitializationContext(EmailTemplateDefinition emailTemplateDefinition,
            IServiceProvider serviceProvider)
        {
            EmailTemplateDefinition = emailTemplateDefinition;
            ServiceProvider = serviceProvider;
        }
    }
}