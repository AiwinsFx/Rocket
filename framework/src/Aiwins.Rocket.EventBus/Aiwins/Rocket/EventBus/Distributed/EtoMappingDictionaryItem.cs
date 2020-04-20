using System;

namespace Aiwins.Rocket.EventBus.Distributed {
    public class EtoMappingDictionaryItem {
        public Type EtoType { get; }

        public Type ObjectMappingContextType { get; }

        public EtoMappingDictionaryItem (Type etoType, Type objectMappingContextType = null) {
            EtoType = etoType;
            ObjectMappingContextType = objectMappingContextType;
        }
    }
}