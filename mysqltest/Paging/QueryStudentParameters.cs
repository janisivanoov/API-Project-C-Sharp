using mysqltest.Enumeration;
using System.Collections.Generic;

namespace mysqltest.Paging
{
    public class QueryStudentParameters : PaginationParameters
    {
        public string FirstName { get; set; } //Creating public string FirstName to use it as a filter

        public string LastName { get; set; } //Creating public string LastName to use it as a filter

        public List<StudentType> Type { get; set; } //Creating public List to check if it contains Type u enter to use it as a filter
    }
}