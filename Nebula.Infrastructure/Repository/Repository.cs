﻿using Microsoft.EntityFrameworkCore;
using Nebula.Application.Interfaces;
using Nebula.Domain.Commons;
using Nebula.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace Nebula.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext appDbContext;
    private readonly DbSet<T> dbSet;
    public Repository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
        this.dbSet = appDbContext.Set<T>();
    }

    public async Task InsertAsync(T entity)
    {
       await this.dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.UpdateAt = DateTime.UtcNow;
        this.appDbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Drop(T entity)
    {
        this.dbSet.Remove(entity);
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression = null!, string[] includes = null!)
    {
        IQueryable<T> query = dbSet.Where(expression);

        if (includes is not null)
            foreach(var include in includes)
                query = query.Include(include);

        return (await query.FirstOrDefaultAsync(expression))!;
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!, bool isNoTracking = true, string[] includes = null!)
    {
        IQueryable<T> query = expression is null ? dbSet : dbSet.Where(expression);

        query = isNoTracking ? query.AsNoTracking() : query;

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }

    public async Task<T> GetEntityWithRelatedAsync(long entityId, params Expression<Func<T, object>>[] includes)
    {
        var query = dbSet.Where(e => EF.Property<long>(e, "Id") == entityId);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return (await query.FirstOrDefaultAsync())!;
    }

    public async Task SaveAsync()
    {
        await this.appDbContext.SaveChangesAsync();
    }
}
