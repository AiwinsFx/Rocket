namespace Aiwins.Rocket.Account.Web.Areas.Account.Controllers.Models {
    public class RocketLoginResult {
        public RocketLoginResult (LoginResultType result) {
            Result = result;
        }

        public LoginResultType Result { get; }

        public string Description => Result.ToString ();
    }
}