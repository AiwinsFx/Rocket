using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Aiwins.Rocket.AspNetCore.Mvc {
    public static class ActionResultHelper {
        public static List<Type> ObjectResultTypes { get; }

        static ActionResultHelper () {
            ObjectResultTypes = new List<Type> {
                typeof (JsonResult),
                typeof (ObjectResult),
                typeof (NoContentResult)
            };
        }

        public static bool IsObjectResult (Type returnType) {
            returnType = AsyncHelper.UnwrapTask (returnType);

            if (!typeof (IActionResult).IsAssignableFrom (returnType)) {
                return true;
            }

            return ObjectResultTypes.Any (t => t.IsAssignableFrom (returnType));
        }
    }
}