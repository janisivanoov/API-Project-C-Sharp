using mysqltest.Enumeration;

namespace mysqltest.Mapping.DTO
{
    public class VaccinatedDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public VaccineType VaccinatedType { get; set; }
        public VaccinatedStatus VaccinatedStatus { get; set; }
    }
}