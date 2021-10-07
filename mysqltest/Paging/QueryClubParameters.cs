using mysqltest.Enumeration;
using mysqltest.Enums;
using System.Collections.Generic;

namespace mysqltest.Paging
{
    public class QueryClubParameters : PaginationParameters
    {
        public string Name { get; set; } //Creating public string Name to use it as a filter

        public List<ClubType> Type { get; set; }  //Creating public List to check if it contains Type u enter to use it as a filter

        public List<ClubStatus> Status { get; set; } //Creating public List to check if it contains Status u enter to use it as a filter
    }
}