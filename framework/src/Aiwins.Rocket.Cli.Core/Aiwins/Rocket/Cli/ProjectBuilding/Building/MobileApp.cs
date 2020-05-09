using System;
using System.Collections.Generic;
using System.Text;

namespace Aiwins.Rocket.Cli.ProjectBuilding.Building
{
    public enum MobileApp
    {
        None,
        ReactNative
    }

    public static  class MobileAppExtensions{
        public static string GetFolderName(this MobileApp mobileApp)
        {
            switch (mobileApp)
            {
                case MobileApp.ReactNative:
                    return "react-native";
            }

            throw new Exception("Mobile app folder name is not set!");
        }
    }
}
