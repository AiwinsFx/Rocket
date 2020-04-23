using Aiwins.Rocket.UI.Navigation;

namespace Aiwins.Rocket.UI.Navigation
{
    public interface IHasMenuItems
    {
        /// <summary>
        /// Menu items.
        /// </summary>
        ApplicationMenuItemList Items { get; }
    }
}