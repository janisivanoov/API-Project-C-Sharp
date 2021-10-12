using AutoMapper;
using mysqltest.Enums;
using mysqltest.Mapping.DTO;
using mysqltest.Models;
using System;

namespace mysqltest.Mapping
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<Club, ClubDTO>()
                .ForMember(dest => dest.StudentCount,
                           opt => opt.MapFrom(src => src.Students.Count));

            CreateMap<Club, ClubTmpDTO>()
                .ForMember(dest => dest.StudentCount,
                           opt => opt.MapFrom(src => src.Students.Count));

            CreateMap<ClubListingDTO, Club>()
                .ForMember(o => o.Type
                          , ex => ex.MapFrom(o => Enum.Parse(typeof(ClubType), o.ClubType)));
        }
    }
}