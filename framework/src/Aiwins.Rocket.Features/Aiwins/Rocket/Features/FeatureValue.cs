using System;

namespace Aiwins.Rocket.Features
{
    [Serializable]
    public class FeatureValue : NameValue
    {
        public FeatureValue()
        {

        }

        public FeatureValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}