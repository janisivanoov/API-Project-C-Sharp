using mysqltest.Enumeration;
using System;

namespace mysqltest.Mapping
{
    public class ClubEventDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Beggining { get; set; }

        public DateTime Ending { get; set; }

        public ClubEventType Type { get; set; }

        public ClubEventStatus EventStatus { get; set; }
    }
}