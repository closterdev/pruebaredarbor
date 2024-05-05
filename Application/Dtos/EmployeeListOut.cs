using Domain.Entities;
using Shared.Common;

namespace Application.Dtos
{
    public class EmployeeListOut : BaseOut
    {
        public IEnumerable<Employee>? EmployeesList { get; set; }
    }
}