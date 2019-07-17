using System.Linq;
using Mep.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mep.Business.Extensions
{
  public static class IQueryableExtensions
  {
    public static IQueryable<T> WhereIsActiveOrActiveOnly<T>(
      this DbSet<T> query, bool activeOnly) where T : BaseEntity
    {
      return query.Where(model => model.IsActive && activeOnly || !activeOnly);
    }  

    public static IQueryable<T> WhereIsActiveOrActiveOnly<T>(
      this IQueryable<T> query, bool activeOnly) where T : BaseEntity
    {
      return query.Where(model => model.IsActive && activeOnly || !activeOnly);
    }

    public static IQueryable<T> AsNoTracking<T>(
      this IQueryable<T> query, bool asNoTracking) where T : BaseEntity
    {
      return asNoTracking ? query.AsNoTracking() : query;
    }       
  }
}