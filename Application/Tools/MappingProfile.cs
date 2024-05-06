using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Tools
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeAddIn, Employee>();
            CreateMap<EmployeeItemIn, Employee>();
        }
    }
}
