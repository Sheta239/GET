﻿
using System.Linq.Expressions;


namespace GET.Domain.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

      
    }
}
