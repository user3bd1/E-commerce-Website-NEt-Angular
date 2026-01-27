using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvalautor<T> where T : BaseEntity
{
   public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec )
    {
        if(spec.Creteria is not null) query = query.Where(spec.Creteria); 
        if(spec.OrderBy is not null) query = query.OrderBy(spec.OrderBy);
        if(spec.OrderByDescending is not null) query = query.OrderByDescending(spec.OrderByDescending);
        if(spec.IsDistinct) query = query.Distinct();
        if (spec.IsPaginEnabled) query = query.Skip(spec.Skip).Take(spec.Take);
        return query;
    }


    public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, ISpecification<T,TResult> spec )
    {
        if(spec.Creteria is not null) query = query.Where(spec.Creteria); 
        if(spec.OrderBy is not null) query = query.OrderBy(spec.OrderBy);
        if(spec.OrderByDescending is not null) query = query.OrderByDescending(spec.OrderByDescending);

        var selectquery = query as IQueryable<TResult>;
        if (spec.Select is not null ) selectquery = query.Select(spec.Select);
        if(spec.IsDistinct) selectquery = selectquery?.Distinct();
        if (spec.IsPaginEnabled) selectquery = selectquery?.Skip(spec.Skip).Take(spec.Take);
        return selectquery ?? query.Cast<TResult>();
    }
    
}
