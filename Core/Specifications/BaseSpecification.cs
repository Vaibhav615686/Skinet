using System;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
   protected BaseSpecification() : this(null)
   {

   }
   public Expression<Func<T, bool>>? Criteria => criteria;
   public Expression<Func<T, object>>? OrderBy { get; protected set; }
   public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

   public bool? IsDistinct { get; protected set; }

   public int? Take { get; protected set; }

   public int? Skip { get; protected set; }

   public bool IsPaginationEnabled { get; protected set; }

   public IQueryable<T> ApplyCriteria(IQueryable<T> query)
   {
      if (Criteria != null)
      {
         query = query.Where(Criteria);

      }
      return query;
   }
   protected void ApplyPaging(int? skip, int? take)
   {
      if (take!=0)
      {
         Skip = skip;
         Take = take;
         IsPaginationEnabled = true;
      }

   }
   protected void AddOrderBy(Expression<Func<T, object>> orderBy)
   {
      OrderBy = orderBy;
   }
   protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
   {
      OrderByDescending = orderByDescending;
   }

   protected void ApplyDistict()
   {
      IsDistinct = true;
   }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
   protected BaseSpecification() : this(null!)
   {

   }
   public Expression<Func<T, TResult>>? Select { get; protected set; }
   public bool? IsDistinct { get; protected set; }
   protected void AddSelect(Expression<Func<T, TResult>> select)
   {
      Select = select;
   }
   protected void ApplyDistict()
   {
      IsDistinct = true;
   }
}
