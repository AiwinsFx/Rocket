using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aiwins.Rocket.Domain.Entities;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Aiwins.Docs.EntityFrameworkCore;

namespace Aiwins.Docs.Projects
{
    public class EfCoreProjectRepository : EfCoreRepository<IDocsDbContext, Project, Guid>, IProjectRepository
    {
        public EfCoreProjectRepository(IDbContextProvider<IDocsDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Project>> GetListAsync(string sorting, int maxResultCount, int skipCount)
        {
            var projects = await DbSet.OrderBy(sorting ?? "Id desc")
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();

            return projects;
        }

        public async Task<Project> GetByShortNameAsync(string shortName)
        {
            var normalizeShortName = NormalizeShortName(shortName);
            
            var project = await DbSet.FirstOrDefaultAsync(p => p.ShortName == normalizeShortName);

            if (project == null)
            {
                throw new EntityNotFoundException($"Project with the name {shortName} not found!");
            }

            return project;
        }

        public async Task<bool> ShortNameExistsAsync(string shortName)
        {
            var normalizeShortName = NormalizeShortName(shortName);
            
            return await DbSet.AnyAsync(x => x.ShortName == normalizeShortName);
        }
        
        private string NormalizeShortName(string shortName)
        {
            return shortName.ToLower();
        }
    }
}