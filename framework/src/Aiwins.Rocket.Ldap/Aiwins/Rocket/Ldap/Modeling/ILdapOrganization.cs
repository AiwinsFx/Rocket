namespace Aiwins.Rocket.Ldap.Modeling {
    public interface ILdapOrganization : ILdapEntry {
        string OrganizationUnit { get; set; }
    }
}