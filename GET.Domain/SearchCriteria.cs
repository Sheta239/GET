using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GET.Domain
{
    public class SearchCriteria
    {
        public string? Name { get; set; }
        public string? OrName { get; set; }
        public string? Job { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? MinOrders { get; set; }
        public decimal? MaxTotalOrderAmount { get; set; }
        public decimal? MinTotalOrderAmount { get; set; }
    }
}
