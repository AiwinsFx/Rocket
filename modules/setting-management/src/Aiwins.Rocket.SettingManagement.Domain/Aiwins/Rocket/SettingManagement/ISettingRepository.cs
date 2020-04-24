using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aiwins.Rocket.Domain.Repositories;

namespace Aiwins.Rocket.SettingManagement
{
    public interface ISettingRepository : IBasicRepository<Setting, Guid>
    {
        Task<Setting> FindAsync(string name, string providerName, string providerKey);

        Task<List<Setting>> GetListAsync(string providerName, string providerKey);
    }
}