using mysqltest.Enumeration;
using System;
using System.Collections.Generic;

namespace mysqltest.Models
{
    public class ClubEvent : BaseEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Beggining { get; set; }

        public DateTime Ending { get; set; }

        public ClubEventType Type { get; set; }

        public ClubStatus Status { get; set; }

        public ICollection<StudentClub> Students { get; set; }

        public ClubEventStatus EventStatus { get; set; }
    }
}