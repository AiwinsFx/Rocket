using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aiwins.Blogging.EntityFrameworkCore;
using Aiwins.Rocket.Domain.Repositories.EntityFrameworkCore;
using Aiwins.Rocket.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aiwins.Blogging.Comments {
    public class EfCoreCommentRepository : EfCoreRepository<IBloggingDbContext, Comment, Guid>, ICommentRepository {
        public EfCoreCommentRepository (IDbContextProvider<IBloggingDbContext> dbContextProvider) : base (dbContextProvider) { }

        public async Task<List<Comment>> GetListOfPostAsync (Guid postId) {
            return await DbSet
                .Where (a => a.PostId == postId)
                .OrderBy (a => a.CreationTime)
                .ToListAsync ();
        }

        public async Task<int> GetCommentCountOfPostAsync (Guid postId) {
            return await DbSet
                .CountAsync (a => a.PostId == postId);
        }

        public async Task<List<Comment>> GetRepliesOfCommentAsync (Guid id) {
            return await DbSet
                .Where (a => a.RepliedCommentId == id).ToListAsync ();
        }

        public async Task DeleteOfPostAsync (Guid id) {
            await Task.Run (() => {
                var recordsToDelete = DbSet.Where (pt => pt.PostId == id);
                DbSet.RemoveRange (recordsToDelete);
            });
        }
    }
}