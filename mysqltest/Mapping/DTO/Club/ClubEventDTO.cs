using mysqltest.Enumeration;
using mysqltest.Enums;
using mysqltest.Models;
using System;

namespace mysqltest.Mapping
{
    public class ClubEventDTO
    {
        public Club Club { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Beggining { get; set; }

        public DateTime Ending { get; set; }

        public ClubType Type { get; set; }

        public ClubStatus Status { get; set; }

        public int StudentCount { get; set; }

        public ClubEventStatus EventStatus { get; set; }
    }
}