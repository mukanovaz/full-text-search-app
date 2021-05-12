using CrawlerIR2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CrawlerIR2.DB
{
    public class Context : DbContext
    {
        public Context(string table_name) : base(table_name)
        { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
    }

    public static class DbSetExtensions
    {
        public static T AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = predicate != null ? dbSet.Any(predicate) : dbSet.Any();
            return !exists ? dbSet.Add(entity) : null;
        }
    }
}
