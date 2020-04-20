namespace Aiwins.Rocket.Http.Modeling {
    public interface IApiDescriptionModelProvider {
        ApplicationApiDescriptionModel CreateApiModel (ApplicationApiDescriptionModelRequestDto input);
    }
}