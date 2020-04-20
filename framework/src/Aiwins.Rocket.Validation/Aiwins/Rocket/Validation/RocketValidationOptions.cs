using System;
using System.Collections.Generic;
using Aiwins.Rocket.Collections;

namespace Aiwins.Rocket.Validation
{
    public class RocketValidationOptions
    {
        public List<Type> IgnoredTypes { get; }

        public ITypeList<IObjectValidationContributor> ObjectValidationContributors { get; set; }

        public RocketValidationOptions()
        {
            IgnoredTypes = new List<Type>();
            ObjectValidationContributors = new TypeList<IObjectValidationContributor>();
        }
    }
}