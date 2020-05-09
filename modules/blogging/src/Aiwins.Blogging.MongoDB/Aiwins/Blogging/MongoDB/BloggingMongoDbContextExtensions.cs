using System;
using Aiwins.Rocket;
using Aiwins.Rocket.MongoDB;
using Aiwins.Blogging.Blogs;
using Aiwins.Blogging.Comments;
using Aiwins.Blogging.Posts;
using Aiwins.Blogging.Users;

namespace Aiwins.Blogging.MongoDB
{
    public static class BloggingMongoDbContextExtensions
    {
        public static void ConfigureBlogging(
            this IMongoModelBuilder builder,
            Action<RocketMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new BloggingMongoModelBuilderConfigurationOptions(
                BloggingDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);

            builder.Entity<BlogUser>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Users";
            });

            builder.Entity<Blog>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Blogs";
            });

            builder.Entity<Post>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Posts";
            });

            builder.Entity<Tagging.Tag>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Tags";
            });

            builder.Entity<Comment>(b =>
            {
                b.CollectionName = options.CollectionPrefix + "Comments";
            });
        }
    }
}
