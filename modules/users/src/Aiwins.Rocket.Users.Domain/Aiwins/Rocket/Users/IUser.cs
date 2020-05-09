using System;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.MultiTenancy;
using JetBrains.Annotations;

namespace Aiwins.Rocket.Users {
    public interface IUser : IAggregateRoot<Guid>, IMultiTenant {
        string UserName { get; }

        [CanBeNull]
        string Name { get; }

        [CanBeNull]
        string Surname { get; }
        
        [CanBeNull]
        string Email { get; }

        bool EmailConfirmed { get; }

        [CanBeNull]
        string PhoneNumber { get; }

        bool PhoneNumberConfirmed { get; }
    }
}