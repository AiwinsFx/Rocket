using System.Collections.Generic;
using Aiwins.Rocket.Ldap.Modeling;

namespace Aiwins.Rocket.Ldap {
    public interface ILdapManager {
        /// <summary>
        /// 查询组织机构列表。
        /// 
        /// 过滤条件: (&(name=xxx)(objectClass=organizationalUnit)) 指定名称查询
        /// 过滤条件: (&(name=*)(objectClass=organizationalUnit)) 名称通配符查询
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<LdapOrganization> GetOrganizations (string name = null);

        /// <summary>
        /// 查询指定的组织机构。
        /// 
        /// 过滤条件: (&(distinguishedName=xxx)(objectClass=organizationalUnit)) 指定名称查询
        /// 
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        LdapOrganization GetOrganization (string distinguishedName);

        void AddSubOrganization (string organizationName, LdapOrganization parentOrganization);
        void AddSubOrganization (string organizationName, string parentDistinguishedName);

        /// <summary>
        /// 查询用户列表。
        /// 
        /// 过滤条件: (&(name=xxx)(objectCategory=person)(objectClass=user)) 指定用户名查询
        /// 过滤条件: (&(name=*)(objectCategory=person)(objectClass=user)) 用户名通配符查询
        ///
        /// 过滤条件: (&(displayName=xxx)(objectCategory=person)(objectClass=user)) 指定姓名查询
        /// 过滤条件: (&(displayName=*)(objectCategory=person)(objectClass=user)) 姓名通配符查询
        ///
        /// 过滤条件: (&(cn=xxx)(objectCategory=person)(objectClass=user)) 指定名称查询
        /// 过滤条件: (&(cn=*)(objectCategory=person)(objectClass=user)) 名称通配符查询
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="commonName"></param>
        /// <returns></returns>
        IList<LdapUser> GetUsers (string name = null, string displayName = null, string commonName = null);

        /// <summary>
        /// 查询指定用户。
        /// 
        /// 过滤条件: (&(distinguishedName=xxx)(objectCategory=person)(objectClass=user)) 指定名称查询
        /// 
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        LdapUser GetUser (string distinguishedName);

        void AddUserToOrganization (string userName, string password, LdapOrganization parentOrganization);
        void AddUserToOrganization (string userName, string password, string parentDistinguishedName);

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="userDomainName">用户名 </param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Authenticate (string userDomainName, string password);
    }
}