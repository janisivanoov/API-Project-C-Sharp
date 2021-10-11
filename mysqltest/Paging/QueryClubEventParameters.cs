using mysqltest.Enumeration;
using System.Collections.Generic;

namespace mysqltest.Paging
{
    public class QueryClubEventParameters : PaginationParameters
    {
        public string Name { get; set; }

        public List<ClubEventType> Type { get; set; }

        public List<ClubEventStatus> EventStatus { get; set; }
    }
}