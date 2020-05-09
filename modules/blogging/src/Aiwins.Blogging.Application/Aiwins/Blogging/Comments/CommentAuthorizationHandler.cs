using System.Security.Principal;
using System.Threading.Tasks;
using Aiwins.Rocket.Authorization.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Aiwins.Blogging.Comments {
    public class CommentAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Comment> {
        private readonly IPermissionChecker _permissionChecker;

        public CommentAuthorizationHandler (IPermissionChecker permissionChecker) {
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync (
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Comment resource) {
            if (requirement.Name == CommonOperations.Delete.Name && await HasDeletePermission (context, resource)) {
                context.Succeed (requirement);
                return;
            }

            if (requirement.Name == CommonOperations.Update.Name && await HasUpdatePermission (context, resource)) {
                context.Succeed (requirement);
                return;
            }
        }

        private async Task<bool> HasDeletePermission (AuthorizationHandlerContext context, Comment resource) {
            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId ()) {
                return true;
            }

            var result = await _permissionChecker.GetResultAsync (context.User, BloggingPermissions.Comments.Delete);
            if (result?.GrantType == PermissionGrantType.Granted) {
                return true;
            }

            return false;
        }

        private async Task<bool> HasUpdatePermission (AuthorizationHandlerContext context, Comment resource) {
            if (resource.CreatorId != null && resource.CreatorId == context.User.FindUserId ()) {
                return true;
            }

            var result = await _permissionChecker.GetResultAsync (context.User, BloggingPermissions.Comments.Update);
            if (result?.GrantType == PermissionGrantType.Granted) {
                return true;
            }

            return false;
        }
    }
}