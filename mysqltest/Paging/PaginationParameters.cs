using System;
using System.ComponentModel.DataAnnotations;

namespace mysqltest.Paging
{
    public class PaginationParameters
    {
        private const int maxPageSize = 50; //Setting the max PageSize

        public int PageNumber { get; set; } = 1; //Smallest number of PageNumber a person can enter == 1

        [Range(1, maxPageSize)] //Setting a range
        public int PageSize { get; set; }
    }
}