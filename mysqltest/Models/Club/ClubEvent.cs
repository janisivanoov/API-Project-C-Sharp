using mysqltest.Enumeration;
using mysqltest.Enums;
using System;
using System.Collections.Generic;

namespace mysqltest.Models
{
    public class ClubEvent
    {
        public Club Club { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public DateTime Beggining { get; set; }

        public DateTime Ending { get; set; }

        public ClubType Type { get; set; }

        public ClubStatus Status { get; set; }

        public ICollection<StudentClub> Students { get; set; }
    }
}