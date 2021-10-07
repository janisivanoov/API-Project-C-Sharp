using mysqltest.Enumeration;
using System.Collections.Generic;

namespace mysqltest.Paging
{
    public class QueryVaccinatedParameters : PaginationParameters
    {
        public string Name { get; set; } //Creating public string Name to use it as a filter

        public List<VaccinatedStatus> Status { get; set; } //Creating public List to check if it contains Status u enter to use it as a filter

        public List<VaccineType> Type { get; set; } //Creating public List to check if it contains Type u enter to use it as a filter
    }
}