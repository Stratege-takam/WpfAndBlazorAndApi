using Microsoft.EntityFrameworkCore;

namespace Brewery.Context;

/// <summary>
/// 
/// </summary>
public static class EfCleanHelper
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <typeparam name="T"></typeparam>
    public static void Clear<T>(this DbContext context) where T : class
    {
        DbSet<T> dbSet = context.Set<T>();
        if (dbSet.Any())
        {
            var entries = dbSet.ToList();
            dbSet.RemoveRange(entries);

            context.SaveChanges();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbSet"></param>
    /// <typeparam name="T"></typeparam>
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
    {
        if (dbSet.Any())
        {
            dbSet.RemoveRange(dbSet.ToList());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="tableNameWithSchema"></param>
    /// <returns></returns>
    public static string Truncate(this DbContext context, string tableNameWithSchema)
    {
        string cmd = $"TRUNCATE TABLE { tableNameWithSchema }";
        context.Database.ExecuteSqlRaw(cmd);
        return cmd;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="tableNameWithSchema"></param>
    /// <returns></returns>
    public static string Delete(this DbContext context, string tableNameWithSchema)
    {
        string cmd = $"DELETE FROM { tableNameWithSchema }";
        context.Database.ExecuteSqlRaw(cmd);
        return cmd;
    }
}