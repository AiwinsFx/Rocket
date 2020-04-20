namespace Aiwins.Rocket.Validation
{
    public interface IObjectValidationContributor
    {
        void AddErrors(ObjectValidationContext context);
    }
}