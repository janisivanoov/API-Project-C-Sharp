namespace mysqltest.Models.Users
{
    public class UpdateRequest
    {
        public Student FirstName { get; set; }
        public Student LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}