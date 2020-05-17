using System;
using System.Collections.Generic;
using System.Linq;
using Aiwins.Rocket.Domain.Entities.Auditing;
using JetBrains.Annotations;
using Aiwins.Rocket.Pinyin;

namespace Aiwins.Rocket.TenantManagement {
    public class Tenant : FullAuditedAggregateRoot<Guid>, IPySpelling {
        public virtual string Name { get; set; }
        [DisableAuditing]
        public virtual string FullPySpelling { get; set; }
        [DisableAuditing]
        public virtual string FirstPySpelling { get; set; }

        public virtual List<TenantConnectionString> ConnectionStrings { get; protected set; }

        protected Tenant () {

        }

        protected internal Tenant (Guid id, [NotNull] string name) : base (id) {
            SetName (name);

            ConnectionStrings = new List<TenantConnectionString> ();
        }

        [CanBeNull]
        public virtual string FindDefaultConnectionString () {
            return FindConnectionString (Data.ConnectionStrings.DefaultConnectionStringName);
        }

        [CanBeNull]
        public virtual string FindConnectionString (string name) {
            return ConnectionStrings.FirstOrDefault (c => c.Name == name)?.Value;
        }

        public virtual void SetDefaultConnectionString (string connectionString) {
            SetConnectionString (Data.ConnectionStrings.DefaultConnectionStringName, connectionString);
        }

        public virtual void SetConnectionString (string name, string connectionString) {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault (x => x.Name == name);

            if (tenantConnectionString != null) {
                tenantConnectionString.SetValue (connectionString);
            } else {
                ConnectionStrings.Add (new TenantConnectionString (Id, name, connectionString));
            }
        }

        public virtual void RemoveDefaultConnectionString () {
            RemoveConnectionString (Data.ConnectionStrings.DefaultConnectionStringName);
        }

        public virtual void RemoveConnectionString (string name) {
            var tenantConnectionString = ConnectionStrings.FirstOrDefault (x => x.Name == name);

            if (tenantConnectionString != null) {
                ConnectionStrings.Remove (tenantConnectionString);
            }
        }

        protected internal virtual void SetName ([NotNull] string name) {
            Name = Check.NotNullOrWhiteSpace (name, nameof (name), TenantConsts.MaxNameLength);
        }
    }
}