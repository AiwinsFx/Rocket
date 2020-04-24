using System;
using JetBrains.Annotations;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.MultiTenancy;

namespace Aiwins.Rocket.Users
{
    public interface IUser : IAggregateRoot<Guid>, IMultiTenant
    {
        string UserName { get; }

        [CanBeNull]
        string Email { get; }

        [CanBeNull]
        string Name  { get; }

        [CanBeNull]
        string Surname { get; }

        bool EmailConfirmed { get; }

        [CanBeNull]
        string PhoneNumber { get; }

        bool PhoneNumberConfirmed { get; }
    }
}