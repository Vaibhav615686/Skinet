using System;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvalutor<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        if (spec.IsPaginationEnabled)
        {
            query = query.Skip((int)spec.Skip).Take((int)spec.Take);
        }
        return query;
    }
    
    public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        var selectQuery = query as IQueryable<TResult>;
        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);
        }
        if ((bool)spec.IsDistinct)
        {
            selectQuery = selectQuery.Distinct();
        }
         if (spec.IsPaginationEnabled)
        {
            selectQuery = selectQuery.Skip((int)spec.Skip).Take((int)spec.Take);
        }
        return selectQuery ;
    }
}
