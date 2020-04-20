namespace Aiwins.Rocket.Modularity {
    public abstract class ModuleLifecycleContributorBase : IModuleLifecycleContributor {
        public virtual void Initialize (ApplicationInitializationContext context, IRocketModule module) { }

        public virtual void Shutdown (ApplicationShutdownContext context, IRocketModule module) { }
    }
}