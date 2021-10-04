namespace mysqltest.Paging
{
    public class QueryStudentParameters : PaginationParameters
    {
        public string FirstName { get; set; } //Creating public string FirstName to use it as a filter

        public string LastName { get; set; } //Creating public string LastName to use it as a filter
    }
}