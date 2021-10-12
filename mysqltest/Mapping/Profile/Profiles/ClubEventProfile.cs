using AutoMapper;
using mysqltest.Models;

namespace mysqltest.Mapping
{
    public class ClubEventProfile : Profile
    {
        public ClubEventProfile()
        {
            CreateMap<ClubEvent, ClubEventProfile>();
        }
    }
}