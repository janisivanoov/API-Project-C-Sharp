using mysqltest.Enumeration;

namespace mysqltest.Mapping.DTO
{
    public class EmployeeDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeType Type { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}