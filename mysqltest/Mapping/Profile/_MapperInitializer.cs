﻿using AutoMapper;

namespace mysqltest.Mapping
{
    public static class MapperInitializer
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClubProfile>();
                cfg.AddProfile<StudentProfile>();
                cfg.AddProfile<VaccinateProfile>();
            });

            return config.CreateMapper();
        }
    }
}