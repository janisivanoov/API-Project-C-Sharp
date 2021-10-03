﻿using mysqltest.Enums;
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

            context.SaveChanges();
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
            context.Add(new Student() { Id = 1, FirstName = "Ali", LastName = "Tayari", BirthDate = new DateTime(1999, 8, 1), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 2, FirstName = "Dmitry", LastName = "Apraksin", BirthDate = new DateTime(1963, 8, 9), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 3, FirstName = "Ivan", LastName = "Ivanou", BirthDate = new DateTime(2004, 8, 12), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 4, FirstName = "Petr", LastName = "Petrov", BirthDate = new DateTime(2000, 1, 1), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 5, FirstName = "Sidor", LastName = "Sidorov", BirthDate = new DateTime(1989, 2, 3), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 6, FirstName = "Pambos", LastName = "Boss", BirthDate = new DateTime(2000, 5, 10), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 7, FirstName = "Christos", LastName = "Christou", BirthDate = new DateTime(1998, 4, 5), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Student() { Id = 8, FirstName = "Savvas", LastName = "Savvou", BirthDate = new DateTime(1999, 12, 21), CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
        }

        private static void InsertClubs(ClubsContext context)
        {
            context.Add(new Club() { Id = 1, Name = "International", Type = ClubType.Other, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 2, Name = "Math", Type = ClubType.Academic, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 3, Name = "Diving", Type = ClubType.Sport, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
            context.Add(new Club() { Id = 4, Name = "Strollers", Type = ClubType.Leisure, CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now });
        }
    }
}