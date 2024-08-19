using GET.Application.DTOs;
using GET.Domain;
using GET.Domain.Interfaces;
using GET.Domain.Models;
using GET.Domain.Specifications;

namespace GET.Application.UseCases
{
    public class SearchCustomers
    {
        private  IUnitOfWork _unitOfWork;

        public SearchCustomers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerDto>> ExecuteAsync(SearchCriteria criteria)
        {
            var spec = new CustomerWithOrdersSpecification(criteria);
            var customers = await _unitOfWork.Repository<Customer>().GetAllWithSpecAsync(spec);
            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Job = c.Job,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                NumberOfOrders = c.Orders == null ? 0 : c.Orders.Count(),
                TotalOrderAmount = c.Orders == null ? 0 : c.Orders?.Sum(o => o.TotalAmount)
            });
        }
    }
}