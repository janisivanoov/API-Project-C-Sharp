using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace mysqltest.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdatedDate { get; set; }
    }
}