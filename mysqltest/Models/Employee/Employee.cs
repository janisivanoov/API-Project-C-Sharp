using mysqltest.Enumeration;

namespace mysqltest.Models
{
    public class Employee : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public EmployeeType Type { get; set; }

        public EmployeeStatus Status { get; set; }
    }
}