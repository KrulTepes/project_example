using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vtbbook.Core.DataAccess.Models;

namespace vtbbook.Core.DataAccess
{
    public class DataContext : IdentityDbContext, IDataContext
    {
        public DataContext(
            DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder
                .UseNpgsql("Server=85.185.82.229;User Id=some_user;Password=some_password;Port=5432;Database=some_bd;"));
        }

        public IQueryable<T> GetQueryable<T>(bool trackChanges = true, bool disabled = false) where T : class, new()
        {
            return GetQueryable<T>(null, trackChanges);
        }

        private IQueryable<T> GetQueryable<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = GetQueryableNonAudit(expression, trackChanges);

            return query;
        }

        private IQueryable<T> GetQueryableNonAudit<T>(Expression<Func<T, bool>> expression, bool trackChanges = true)
            where T : class, new()
        {
            var query = trackChanges
                ? Set<T>().AsQueryable()
                : Set<T>().AsNoTracking();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public ICollection<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return SqlQuery<T>(sql, parameters).ToList();
        }

        public T Delete<T>(T item) where T : class, new()
        {
            return Set<T>().Remove(item).Entity;
        }

        public void SqlCommand(string sql, params object[] parameters)
        {
            SqlCommand(sql, parameters);
        }

        public void DeleteRange<T>(IEnumerable<T> item) where T : class, new()
        {
            Set<T>().RemoveRange(item);
        }

        public T Insert<T>(T item) where T : class, new()
        {
            return PerformAction(item, EntityState.Added);
        }

        public IEnumerable<T> InsertMany<T>(IEnumerable<T> items) where T : class, new()
        {
            var result = new List<T>();
            foreach (var item in items)
            {
                result.Add(PerformAction(item, EntityState.Added));
            }
            return result;
        }
        public new T Update<T>(T item) where T : class, new()
        {
            return PerformAction(item, EntityState.Modified);
        }

        public IEnumerable<T> UpdateMany<T>(IEnumerable<T> items) where T : class, new()
        {
            var result = new List<T>();
            foreach (var item in items)
            {
                result.Add(PerformAction(item, EntityState.Modified));
            }
            return result;
        }

        protected virtual TItem PerformAction<TItem>(TItem item, EntityState entityState) where TItem : class, new()
        {
            Entry(item).State = entityState;
            return item;
        }

        public int Save()
        {
            int changes;
            try
            {
                AddTimestamps();
                changes = SaveChanges();
            }
            catch (Exception e)
            {
                //_logger.LogError(e, "Save error.");
                throw;
            }
            return changes;
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}
