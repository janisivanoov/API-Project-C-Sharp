using mysqltest.Enums;

namespace mysqltest.Mapping.DTO
{
    public class ClubTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClubType ClubType { get; set; }
    }
}