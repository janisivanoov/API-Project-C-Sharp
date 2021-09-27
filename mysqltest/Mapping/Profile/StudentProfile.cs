using AutoMapper;
using mysqltest.Mapping.DTO;
using mysqltest.Models;

namespace mysqltest.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDTO>()
                             .ForMember(dest => dest.ClubCount,
                                        opt => opt.MapFrom(src => src.Clubs.Count));

            CreateMap<Student, StudentListingDTO>();
        }
    }
}