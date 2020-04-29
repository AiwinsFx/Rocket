using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Aiwins.Rocket.SettingManagement.MongoDB {
    public class MongoSettingRepository : MongoDbRepository<ISettingManagementMongoDbContext, Setting, Guid>, ISettingRepository {
        public MongoSettingRepository (IMongoDbContextProvider<ISettingManagementMongoDbContext> dbContextProvider) : base (dbContextProvider) {

        }

        public virtual async Task<Setting> FindAsync (string name, string providerName, string providerKey) {
            return await GetMongoQueryable ().FirstOrDefaultAsync (s => s.Name == name && s.ProviderName == providerName && s.ProviderKey == providerKey);
        }

        public virtual async Task<List<Setting>> GetListAsync (string providerName, string providerKey) {
            return await GetMongoQueryable ().Where (s => s.ProviderName == providerName && s.ProviderKey == providerKey).ToListAsync ();
        }
    }
}