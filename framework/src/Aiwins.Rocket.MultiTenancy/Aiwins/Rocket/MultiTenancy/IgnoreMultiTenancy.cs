using System;

namespace Aiwins.Rocket.MultiTenancy {
    [AttributeUsage (AttributeTargets.All)]
    public class IgnoreMultiTenancyAttribute : Attribute {

    }
}