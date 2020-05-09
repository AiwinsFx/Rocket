using System;
using System.Runtime.Serialization;
using Aiwins.Rocket;

namespace Aiwins.Docs
{
    [Serializable]
    public class ResourceNotFoundException : BusinessException
    {
        public string ResourceName { get; set; }

        public ResourceNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        public ResourceNotFoundException(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}