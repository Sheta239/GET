using GET.Domain.Models;
using LinqKit;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace GET.Domain.Specifications
{
    public class CustomerWithOrdersSpecification : BaseSpecification<Customer>
    {
        public CustomerWithOrdersSpecification(SearchCriteria criteria)
            : base(BuildPredicate(criteria))
        {
        }


        private static Expression<Func<Customer, bool>> BuildPredicate(SearchCriteria criteria)
        {
            var predicate = PredicateBuilder.New<Customer>(true);

            if (!string.IsNullOrEmpty(criteria.Name))
            {

                if (!string.IsNullOrEmpty(criteria.OrName))
                {
                    predicate = predicate.And(c => c.Name.Contains(criteria.Name) || c.Name.Contains(criteria.OrName));

                }
                else
                {
                    predicate = predicate.And(c => c.Name.Contains(criteria.Name));
                }
            }

            if (!string.IsNullOrEmpty(criteria.Job))
            {
                predicate = predicate.And(c => c.Job.Contains(criteria.Job));
            }

            if (!string.IsNullOrEmpty(criteria.Address))
            {
                predicate = predicate.And(c => c.Address.Contains(criteria.Address));
            }

            if (!string.IsNullOrEmpty(criteria.Phone))
            {
                predicate = predicate.And(c => c.Phone.Contains(criteria.Phone));
            }

            if (!string.IsNullOrEmpty(criteria.Email))
            {
                predicate = predicate.And(c => c.Email.Contains(criteria.Email));
            }

            if (criteria.DateOfBirth != null)
            {
                predicate = predicate.And(c => c.DateOfBirth == criteria.DateOfBirth);
            }

            if (criteria.MinOrders.HasValue)
            {
                predicate = predicate.And(c => c.Orders.Count >= criteria.MinOrders.Value);
            }

            if (criteria.MaxTotalOrderAmount.HasValue)
            {
                predicate = predicate.And(c => c.Orders.Sum(o => o.TotalAmount) <= criteria.MaxTotalOrderAmount.Value);
            }

            if (criteria.MinTotalOrderAmount.HasValue)
            {
                predicate = predicate.And(c => c.Orders.Sum(o => o.TotalAmount) >= criteria.MinTotalOrderAmount.Value);
            }

            return predicate;
        }
    }
}
