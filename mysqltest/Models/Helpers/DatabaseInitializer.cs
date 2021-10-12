using mysqltest.Enums;
using System;
using System.Linq;

namespace mysqltest.Models.Helpers
{
    public class DatabaseInitializer
    {
        public static void InitializeIfNeeded(ClubsContext context)
        {
            if (context.Clubs.Any())
                return;

            InsertClubs(context);
            InsertStudents(context);
            InsertStudentClubs(context);
            InsertLoginUp(context);
            InsertVaccinated(context);
            InsertClubEvent(context);
            InsertEmployee(context);

            context.SaveChanges();
        }

        private static void InsertEmployee(ClubsContext context)
        {
            context.Add(new Employee() { FirstName = "Ali", LastName = "Tayari", Id = 1, Status = Enumeration.EmployeeStatus.Active, Type = Enumeration.EmployeeType.Worker });
        }

        private static void InsertClubEvent(ClubsContext context)
        {
            context.Add(new ClubEvent() { Name = "First Event", Id = 1, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Beggining = new DateTime(2021, 10, 9), Ending = new DateTime(2022, 10, 9), EventStatus = Enumeration.ClubEventStatus.Will_be, Status = Enumeration.ClubStatus.Active, Type = Enumeration.ClubEventType.Active });
        }

        private static void InsertLoginUp(ClubsContext context)
        {
            context.Add(new User() { FirstName = "Ali", LastName = "Tayari", Id = 1, PasswordHash = "12345", Username = "tester", Password = "12345" });
            context.Add(new User() { FirstName = "Ivan", LastName = "Ivanov", Id = 2, PasswordHash = "12345", Username = "ivan", Password = "12345" });
        }

        private static void InsertStudentClubs(ClubsContext context)
        {
            context.Add(new StudentClub() { Id = 1, ClubId = 1, StudentId = 1 });
            context.Add(new StudentClub() { Id = 2, ClubId = 2, StudentId = 1 });
            context.Add(new StudentClub() { Id = 3, ClubId = 3, StudentId = 2 });
            context.Add(new StudentClub() { Id = 4, ClubId = 4, StudentId = 3 });
            context.Add(new StudentClub() { Id = 5, ClubId = 3, StudentId = 4 });
            context.Add(new StudentClub() { Id = 6, ClubId = 2, StudentId = 5 });
            context.Add(new StudentClub() { Id = 7, ClubId = 1, StudentId = 6 });
            context.Add(new StudentClub() { Id = 8, ClubId = 2, StudentId = 7 });
            context.Add(new StudentClub() { Id = 9, ClubId = 3, StudentId = 8 });
            context.Add(new StudentClub() { Id = 10, ClubId = 4, StudentId = 3 });
            context.Add(new StudentClub() { Id = 11, ClubId = 2, StudentId = 5 });
            context.Add(new StudentClub() { Id = 12, ClubId = 2, StudentId = 6 });
            context.Add(new StudentClub() { Id = 13, ClubId = 1, StudentId = 7 });
        }

        private static void InsertStudents(ClubsContext context)
        {
            context.Add(new Student() { Id = 1, FirstName = "Ali", LastName = "Tayari", BirthDate = new DateTime(1999, 8, 1), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.Active });
            context.Add(new Student() { Id = 2, FirstName = "Dmitry", LastName = "Apraksin", BirthDate = new DateTime(1963, 8, 9), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 3, FirstName = "Ivan", LastName = "Ivanou", BirthDate = new DateTime(2004, 8, 12), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.Active });
            context.Add(new Student() { Id = 4, FirstName = "Petr", LastName = "Petrov", BirthDate = new DateTime(2000, 1, 1), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.NoAnswer });
            context.Add(new Student() { Id = 5, FirstName = "Sidor", LastName = "Sidorov", BirthDate = new DateTime(1989, 2, 3), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.Active });
            context.Add(new Student() { Id = 6, FirstName = "Pambos", LastName = "Boss", BirthDate = new DateTime(2000, 5, 10), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.NoAnswer });
            context.Add(new Student() { Id = 7, FirstName = "Christos", LastName = "Christou", BirthDate = new DateTime(1998, 4, 5), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.InActive });
            context.Add(new Student() { Id = 8, FirstName = "Savvas", LastName = "Savvou", BirthDate = new DateTime(1999, 12, 21), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Type = Enumeration.StudentType.InActive });
        }

        private static void InsertClubs(ClubsContext context)
        {
            context.Add(new Club() { Id = 1, Name = "International", Type = ClubType.Other, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 2, Name = "Math", Type = ClubType.Academic, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 3, Name = "Diving", Type = ClubType.Sport, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 4, Name = "Strollers", Type = ClubType.Leisure, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
        }

        private static void InsertVaccinated(ClubsContext context)
        {
            context.Add(new VaccinatedUser() { Id = 1, FirstName = "Ivan", LastName = "Ivanou", Date_Of_Birth = new DateTime(2004, 8, 12), Id_Card_Number = 123456789, PhoneNumber = 99633191, VaccinatedStatus = Enumeration.VaccinatedStatus.Vaccinated, VaccinatedType = Enumeration.VaccineType.Pfizer, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new VaccinatedUser() { Id = 2, FirstName = "Ali", LastName = "Tayari", Date_Of_Birth = new DateTime(1999, 8, 1), Id_Card_Number = 987654321, PhoneNumber = 19133699, VaccinatedStatus = Enumeration.VaccinatedStatus.First_dose, VaccinatedType = Enumeration.VaccineType.Moderna, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
        }
    }
}