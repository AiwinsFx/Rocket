using System;
using Aiwins.Rocket.EventBus;

namespace Aiwins.Rocket.Users
{
    [EventName("Aiwins.Rocket.Users.User")]
    public class UserEto : IUserData
    {
        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
    }
}
