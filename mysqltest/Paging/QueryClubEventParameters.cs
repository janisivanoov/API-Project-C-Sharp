using mysqltest.Enumeration;
using System;
using System.Collections.Generic;

namespace mysqltest.Paging
{
    public class QueryClubEventParameters : PaginationParameters
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public List<ClubEventType> Type { get; set; }

        public DateTime Beggining { get; set; }

        public DateTime Ending { get; set; }

        public List<ClubEventStatus> EventStatus { get; set; }
    }
}