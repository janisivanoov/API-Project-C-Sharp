using AutoMapper;
using mysqltest.Mapping.DTO;
using mysqltest.Models;

namespace mysqltest.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}