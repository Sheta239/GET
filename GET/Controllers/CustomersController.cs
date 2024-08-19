using GET.Application.UseCases;
using GET.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SearchCustomers _searchCustomers;

        public CustomersController(SearchCustomers searchCustomers)
        {
            _searchCustomers = searchCustomers;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCriteria criteria)
        {
            var result = await _searchCustomers.ExecuteAsync(criteria);
            return Ok(result);
        }
    }
}
