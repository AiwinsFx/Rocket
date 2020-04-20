using System.Collections.Generic;

namespace Aiwins.Rocket.Validation.StringValues
{
    public interface ISelectionStringValueItemSource
    {
        ICollection<ISelectionStringValueItem> Items { get; }
    }
}