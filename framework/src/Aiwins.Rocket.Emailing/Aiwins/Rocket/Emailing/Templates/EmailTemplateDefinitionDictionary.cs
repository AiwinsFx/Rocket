using System.Collections.Generic;

namespace Aiwins.Rocket.Emailing.Templates {
    public class EmailTemplateDefinitionDictionary : Dictionary<string, EmailTemplateDefinition> {
        public EmailTemplateDefinitionDictionary Add (EmailTemplateDefinition emailTemplateDefinition) {
            if (ContainsKey (emailTemplateDefinition.Name)) {
                throw new RocketException (
                    "There is already an email template definition with given name: " +
                    emailTemplateDefinition.Name
                );
            }

            this [emailTemplateDefinition.Name] = emailTemplateDefinition;

            return this;
        }
    }
}