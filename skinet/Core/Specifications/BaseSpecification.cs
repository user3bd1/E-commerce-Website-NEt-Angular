using System.Dynamic;
using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T,bool>>?creteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) {}
    public Expression<Func<T, bool>>? Creteria => creteria;

    public Expression<Func<T, object>>? OrderBy  {get; private set;}

    public Expression<Func<T, object>>? OrderByDescending {get; private set;}

    public bool IsDistinct {get; private set;}

    public int Take {get; private set;}

    public int Skip {get; private set;}

    public bool IsPaginEnabled {get; private set;}

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(Creteria is not null) query = query.Where(Creteria);
        return query;
    }

    protected void AddOrderBy(Expression<Func<T,object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }

     protected void AddOrderByDescending(Expression<Func<T,object>> OrderByDescExpression)
    {
        OrderByDescending = OrderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPaginEnabled = true;
    }
}


public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null!) {}
    public Expression<Func<T, TResult>>? Select {get; private set;}

    protected void AddSelect(Expression<Func<T,TResult>> SelectExpression)
    {
        Select = SelectExpression;
    }
}
