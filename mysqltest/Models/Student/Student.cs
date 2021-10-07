using mysqltest.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mysqltest.Models
{
    public class Student : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<StudentClub> Clubs { get; set; }

        public StudentType Type { get; set; }
    }
}