using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories.MongoDB;
using Aiwins.Rocket.MongoDB;
using Aiwins.Docs.MongoDB;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Aiwins.Docs.Projects
{
    public class MongoProjectRepository : MongoDbRepository<IDocsMongoDbContext, Project, Guid>, IProjectRepository
    {
        public MongoProjectRepository(IMongoDbContextProvider<IDocsMongoDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        public async Task<List<Project>> GetListAsync(string sorting, int maxResultCount, int skipCount)
        {
            var projects = await GetMongoQueryable().OrderBy(sorting ?? "Id desc").As<IMongoQueryable<Project>>()
                .PageBy<Project, IMongoQueryable<Project>>(skipCount, maxResultCount)
                .ToListAsync();

            return projects;
        }

        public async Task<Project> GetByShortNameAsync(string shortName)
        {
            var normalizeShortName = NormalizeShortName(shortName);
            
            var project = await GetMongoQueryable().FirstOrDefaultAsync(p => p.ShortName == normalizeShortName);

            if (project == null)
            {
                throw new EntityNotFoundException($"Project with the name {shortName} not found!");
            }

            return project;
        }

        public async Task<bool> ShortNameExistsAsync(string shortName)
        {
            var normalizeShortName = NormalizeShortName(shortName);
            
            return await GetMongoQueryable().AnyAsync(x => x.ShortName == normalizeShortName);
        }
        
        private string NormalizeShortName(string shortName)
        {
            return shortName.ToLower();
        }
    }
}