using mysqltest.Enumeration;
using mysqltest.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mysqltest.Models
{
    public class Club : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ClubType Type { get; set; }

        public ICollection<StudentClub> Students { get; set; }

        public ClubStatus Status { get; set; }
    }
}