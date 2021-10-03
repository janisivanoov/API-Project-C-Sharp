using System;

namespace mysqltest.Mapping.DTO
{
    public class StudentListingDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}