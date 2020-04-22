using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;
using Aiwins.Rocket.DependencyInjection;
using Aiwins.Rocket.ExceptionHandling;
using Aiwins.Rocket.Ldap.Exceptions;
using Aiwins.Rocket.Ldap.Modeling;

namespace Aiwins.Rocket.Ldap {
    public class LdapManager : ILdapManager, ITransientDependency {
        private readonly string _searchBase;
        private readonly RocketLdapOptions _ldapOptions;
        private readonly IHybridServiceScopeFactory _hybridServiceScopeFactory;

        private readonly string[] _attributes = {
            "objectCategory",
            "objectClass",
            "cn",
            "name",
            "distinguishedName",
            "ou",
            "sAMAccountName",
            "userPrincipalName",
            "telephoneNumber",
            "mail"
        };

        public LdapManager (IOptions<RocketLdapOptions> ldapSettingsOptions, IHybridServiceScopeFactory hybridServiceScopeFactory) {
            _hybridServiceScopeFactory = hybridServiceScopeFactory;
            _ldapOptions = ldapSettingsOptions.Value;
            _searchBase = _ldapOptions.SearchBase;
        }

        #region Organization
        /// <summary>
        /// 查询组织机构列表。
        /// 
        /// 过滤条件: (&(name=xxx)(objectClass=organizationalUnit)) 指定名称查询
        /// 过滤条件: (&(name=*)(objectClass=organizationalUnit)) 名称通配符查询
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<LdapOrganization> GetOrganizations (string name = null) {
            var conditions = new Dictionary<string, string> { { "name", name },
            { "objectClass", "organizationalUnit" },
                };
            return Query<LdapOrganization> (_searchBase, conditions);
        }

        /// <summary>
        /// 查询指定的组织机构。
        /// 
        /// 过滤条件: (&(distinguishedName=xxx)(objectClass=organizationalUnit)) 指定名称查询
        /// 
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        public LdapOrganization GetOrganization (string distinguishedName) {
            distinguishedName = Check.NotNullOrWhiteSpace (distinguishedName, nameof (distinguishedName));
            var conditions = new Dictionary<string, string> { { "distinguishedName", distinguishedName },
                    { "objectClass", "organizationalUnit" },
                };
            return QueryOne<LdapOrganization> (_searchBase, conditions);
        }

        public void AddSubOrganization (string organizationName, LdapOrganization parentOrganization) {
            organizationName = Check.NotNullOrWhiteSpace (organizationName, nameof (organizationName));
            var dn = $"OU={organizationName},{parentOrganization.DistinguishedName}";

            var attributeSet = new LdapAttributeSet {
                new LdapAttribute ("objectCategory", $"CN=Organizational-Unit,CN=Schema,CN=Configuration,{_ldapOptions.DomainDistinguishedName}"),
                new LdapAttribute ("objectClass", new [] { "top", "organizationalUnit" }),
                new LdapAttribute ("name", organizationName),
            };

            var newEntry = new LdapEntry (dn, attributeSet);

            using (var ldapConnection = GetConnection ()) {
                ldapConnection.Add (newEntry);
            }
        }

        public void AddSubOrganization (string organizationName, string parentDistinguishedName) {
            organizationName = Check.NotNullOrWhiteSpace (organizationName, nameof (organizationName));
            parentDistinguishedName =
                Check.NotNullOrWhiteSpace (parentDistinguishedName, nameof (parentDistinguishedName));

            var parentOrganization = GetOrganization (parentDistinguishedName);
            if (null == parentOrganization) {
                throw new OrganizationNotExistException (parentDistinguishedName);
            }

            AddSubOrganization (organizationName, parentOrganization);
        }

        #endregion

        #region User
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
        public IList<LdapUser> GetUsers (string name = null, string displayName = null, string commonName = null) {
            var conditions = new Dictionary<string, string> { { "objectCategory", "person" },
            { "objectClass", "user" },
            { "name", name },
            { "displayName", displayName },
            { "cn", commonName },
                };
            return Query<LdapUser> (_searchBase, conditions);
        }

        /// <summary>
        /// 查询指定用户。
        /// 
        /// 过滤条件: (&(distinguishedName=xxx)(objectCategory=person)(objectClass=user)) 指定名称查询
        /// 
        /// </summary>
        /// <param name="distinguishedName"></param>
        /// <returns></returns>
        public LdapUser GetUser (string distinguishedName) {
            distinguishedName = Check.NotNullOrWhiteSpace (distinguishedName, nameof (distinguishedName));
            var conditions = new Dictionary<string, string> { { "objectCategory", "person" },
                    { "objectClass", "user" },
                    { "distinguishedName", distinguishedName },
                };
            return QueryOne<LdapUser> (_searchBase, conditions);
        }

        public void AddUserToOrganization (string userName, string password, LdapOrganization parentOrganization) {
            var dn = $"CN={userName},{parentOrganization.DistinguishedName}";
            var mail = $"{userName}@{_ldapOptions.DomainName}";
            sbyte[] encodedBytes = SupportClass.ToSByteArray (Encoding.Unicode.GetBytes ($"\"{password}\""));

            var attributeSet = new LdapAttributeSet {
                new LdapAttribute ("instanceType", "4"),
                new LdapAttribute ("objectCategory", $"CN=Person,CN=Schema,CN=Configuration,{_ldapOptions.DomainDistinguishedName}"),
                new LdapAttribute ("objectClass", new [] { "top", "person", "organizationalPerson", "user" }),
                new LdapAttribute ("name", userName),
                new LdapAttribute ("cn", userName),
                new LdapAttribute ("sAMAccountName", userName),
                new LdapAttribute ("userPrincipalName", userName),
                new LdapAttribute ("sn", userName),
                new LdapAttribute ("displayName", userName),
                new LdapAttribute ("unicodePwd", encodedBytes),
                new LdapAttribute ("userAccountControl", "512"),
                new LdapAttribute ("mail", mail),
            };
            var newEntry = new LdapEntry (dn, attributeSet);

            using (var ldapConnection = GetConnection ()) {
                ldapConnection.Add (newEntry);
            }
        }

        public void AddUserToOrganization (string userName, string password, string parentDistinguishedName) {
            var dn = $"CN={userName},{parentDistinguishedName}";
            var mail = $"{userName}@{_ldapOptions.DomainName}";
            sbyte[] encodedBytes = SupportClass.ToSByteArray (Encoding.Unicode.GetBytes ($"\"{password}\""));

            var attributeSet = new LdapAttributeSet {
                new LdapAttribute ("instanceType", "4"),
                new LdapAttribute ("objectCategory", $"CN=Person,CN=Schema,CN=Configuration,{_ldapOptions.DomainDistinguishedName}"),
                new LdapAttribute ("objectClass", new [] { "top", "person", "organizationalPerson", "user" }),
                new LdapAttribute ("name", userName),
                new LdapAttribute ("cn", userName),
                new LdapAttribute ("sAMAccountName", userName),
                new LdapAttribute ("userPrincipalName", userName),
                new LdapAttribute ("sn", userName),
                new LdapAttribute ("displayName", userName),
                new LdapAttribute ("unicodePwd", encodedBytes),
                new LdapAttribute ("userAccountControl", "512"),
                new LdapAttribute ("mail", mail),
            };
            var newEntry = new LdapEntry (dn, attributeSet);

            using (var ldapConnection = GetConnection ()) {
                ldapConnection.Add (newEntry);
            }
        }

        #endregion

        #region Authenticate

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="userDomainName">用户名 </param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Authenticate (string userDomainName, string password) {
            try {
                using (GetConnection (userDomainName, password)) {
                    return true;
                }
            } catch (Exception ex) {
                using (var scope = _hybridServiceScopeFactory.CreateScope ()) {
                    scope.ServiceProvider
                        .GetRequiredService<IExceptionNotifier> ()
                        .NotifyAsync (ex);
                }

                return false;
            }
        }

        #endregion

        private ILdapConnection GetConnection (string bindUserName = null, string bindUserPassword = null) {
            // 认证信息绑定
            bindUserName = bindUserName ?? _ldapOptions.Credentials.DomainUserName;
            bindUserPassword = bindUserPassword ?? _ldapOptions.Credentials.Password;

            var ldapConnection = new LdapConnection () { SecureSocketLayer = _ldapOptions.UseSsl };
            if (_ldapOptions.UseSsl) {
                ldapConnection.UserDefinedServerCertValidationDelegate += (sender, certificate, chain, sslPolicyErrors) => true;
            }
            ldapConnection.Connect (_ldapOptions.ServerHost, _ldapOptions.ServerPort);

            if (_ldapOptions.UseSsl) {
                ldapConnection.Bind (LdapConnection.Ldap_V3, bindUserName, bindUserPassword);
            } else {
                ldapConnection.Bind (bindUserName, bindUserPassword);
            }
            return ldapConnection;
        }

        private IList<T> Query<T> (string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry {
            var filter = LdapHelps.BuildFilter (conditions);

            var result = new List<T> ();

            using (var ldapConnection = GetConnection ()) {
                var search = ldapConnection.Search (searchBase, LdapConnection.SCOPE_SUB, filter,
                    _attributes, false, null, null);

                LdapMessage message;
                while ((message = search.getResponse ()) != null) {
                    if (!(message is LdapSearchResult searchResultMessage)) {
                        continue;
                    }
                    var entry = searchResultMessage.Entry;
                    if (typeof (T) == typeof (LdapOrganization)) {
                        result.Add (new LdapOrganization (entry.getAttributeSet ()) as T);
                    }

                    if (typeof (T) == typeof (LdapUser)) {
                        result.Add (new LdapUser (entry.getAttributeSet ()) as T);
                    }
                }
            }
            return result;
        }

        private T QueryOne<T> (string searchBase, Dictionary<string, string> conditions) where T : class, ILdapEntry {
            var filter = LdapHelps.BuildFilter (conditions);

            using (var ldapConnection = GetConnection ()) {
                var search = ldapConnection.Search (searchBase, LdapConnection.SCOPE_SUB, filter,
                    _attributes, false, null, null);

                LdapMessage message;
                while ((message = search.getResponse ()) != null) {
                    if (!(message is LdapSearchResult searchResultMessage)) {
                        continue;
                    }
                    var entry = searchResultMessage.Entry;
                    if (typeof (T) == typeof (LdapOrganization)) {
                        return new LdapOrganization (entry.getAttributeSet ()) as T;
                    }

                    if (typeof (T) == typeof (LdapUser)) {
                        return new LdapUser (entry.getAttributeSet ()) as T;
                    }
                    return null;
                }
            }
            return null;
        }

    }
}