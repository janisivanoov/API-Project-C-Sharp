using mysqltest.Enumeration;
using mysqltest.Enums;

namespace mysqltest.Mapping.DTO
{
    public class ClubTmpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ClubType Type { get; set; }
        public int StudentCount { get; set; }
        public ClubStatus Status { get; set; }
    }
}