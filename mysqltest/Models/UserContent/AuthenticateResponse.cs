namespace mysqltest.Models.Users
{
    public class AuthenticateResponse
    {
        public Student Id { get; set; }
        public Student FirstName { get; set; }
        public Student LastName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}