using Domain.Entities;
using Shared.Common;

namespace Application.Dtos
{
    public class EmployeeIdOut : BaseOut
    {
        public Employee EmployeeItem { get; set; }
    }
}