using System.ComponentModel.DataAnnotations;

namespace mysqltest.Models.Users
{
    public class RegisterRequest
    {
        [Required]
        public Student FirstName { get; set; }

        [Required]
        public Student LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}