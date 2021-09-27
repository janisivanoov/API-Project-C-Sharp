using System;

namespace mysqltest.Paging
{
    public class QueryStudentParameters : PaginationParameters
    {
        public string Name { get; set; } //Creating public string Name to use it as a filter

        public DateTime BirthDate { get; set; } //Creating public DateTime BirthDate to use it as a filter
    }
}