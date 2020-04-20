using System;
using System.Collections.Generic;
using IdentityModel;

namespace Aiwins.Rocket.IdentityModel {
    public class IdentityClientConfiguration : Dictionary<string, string> {
        /// <summary>
        /// 可取值: "client_credentials" or "password".
        /// 默认值: "client_credentials".
        /// </summary>
        public string GrantType {
            get => this.GetOrDefault (nameof (GrantType));
            set => this [nameof (GrantType)] = value;
        }

        /// <summary>
        /// Client Id.
        /// </summary>
        public string ClientId {
            get => this.GetOrDefault (nameof (ClientId));
            set => this [nameof (ClientId)] = value;
        }

        /// <summary>
        /// Client Secret (未经过Hash处理的普通文本)
        /// </summary>
        public string ClientSecret {
            get => this.GetOrDefault (nameof (ClientSecret));
            set => this [nameof (ClientSecret)] = value;
        }

        /// <summary>
        /// User Name
        /// 仅当 <see cref="GrantType"/> 值为 "password"时生效
        /// </summary>
        public string UserName {
            get => this.GetOrDefault (nameof (UserName));
            set => this [nameof (UserName)] = value;
        }

        /// <summary>
        /// User Password
        /// 仅当 <see cref="GrantType"/> 值为 "password"时生效
        /// </summary>
        public string UserPassword {
            get => this.GetOrDefault (nameof (UserPassword));
            set => this [nameof (UserPassword)] = value;
        }

        /// <summary>
        /// Authority
        /// </summary>
        public string Authority {
            get => this.GetOrDefault (nameof (Authority));
            set => this [nameof (Authority)] = value;
        }

        /// <summary>
        /// Scope
        /// </summary>
        public string Scope {
            get => this.GetOrDefault (nameof (Scope));
            set => this [nameof (Scope)] = value;
        }

        /// <summary>
        /// 是否启用Https
        /// 默认值: true.
        /// </summary>
        public bool RequireHttps {
            get => this.GetOrDefault (nameof (RequireHttps))?.To<bool> () ?? true;
            set => this [nameof (RequireHttps)] = value.ToString ().ToLowerInvariant ();
        }

        public IdentityClientConfiguration () {

        }

        public IdentityClientConfiguration (
            string authority,
            string scope,
            string clientId,
            string clientSecret,
            string grantType = OidcConstants.GrantTypes.ClientCredentials,
            string userName = null,
            string userPassword = null,
            bool requireHttps = true) {
            this [nameof (Authority)] = authority;
            this [nameof (Scope)] = scope;
            this [nameof (ClientId)] = clientId;
            this [nameof (ClientSecret)] = clientSecret;
            this [nameof (GrantType)] = grantType;
            this [nameof (UserName)] = userName;
            this [nameof (UserPassword)] = userPassword;
            this [nameof (RequireHttps)] = requireHttps.ToString ().ToLowerInvariant ();
        }
    }
}