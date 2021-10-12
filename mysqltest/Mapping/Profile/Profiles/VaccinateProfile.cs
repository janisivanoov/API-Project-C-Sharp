using AutoMapper;
using mysqltest.Mapping.DTO;
using mysqltest.Models;

namespace mysqltest.Mapping
{
    public class VaccinateProfile : Profile
    {
        public VaccinateProfile()
        {
            CreateMap<VaccinatedUser, VaccinatedDTO>();
        }
    }
}