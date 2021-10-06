using mysqltest.Enumeration;
using System;
using System.ComponentModel.DataAnnotations;

namespace mysqltest.Models
{
    public class VaccinatedUser
    {
        public long Id { get; set; }

        [Required]
        public long Id_Card_Number { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime Date_Of_Birth { get; set; }

        public VaccineType vaccinatedType { get; set; }
        public VaccinatedStatus VaccinatedStatus { get; set; }
    }
}