﻿using Application.Dtos;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeAddOut> CreateEmployeeAsync(EmployeeAddIn employeeIn);
        Task<EmployeeListOut> GetEmployeeAsync();
    }
}