using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;

namespace Wba.EfCore.StudentApp.Web.Data
{
    public static class DataSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            //create arrays for each entity
            var teachers = new Teacher[]
                {
                    //put teachers here
                    new Teacher{Id=1,Firstname="TopDog",Lastname="Soete" },
                    new Teacher{Id=2,Firstname="Willie",Lastname="Schokellé" },
                    new Teacher{Id=3,Firstname="Sieggie",Lastname="Derdeyn" }
                };

            var students = new Student[]
                {
                    //put students here
                    new Student{Id=1,Firstname="Jimmy",Lastname="Page" },
                    new Student{Id=2,Firstname="Rory",Lastname="Gallagher" },
                    new Student{Id=3,Firstname="Pino",Lastname="Daniele" },
                };
            var courses = new Course[]
                {
                    //put courses here
                    new Course{Id=1,Title="WBA",TeacherId=3 },
                    new Course{Id=2,Title="WFA",TeacherId=2 },
                    new Course{Id=3,Title="DBS",TeacherId=1 }
                };
            var studentCourses = new StudentCourses[]
                {
                    //put studentCourses here
                    new StudentCourses{StudentId=1,CourseId=2},
                    new StudentCourses{StudentId=1,CourseId=1},
                    new StudentCourses{StudentId=1,CourseId=3},
                    new StudentCourses{StudentId=2,CourseId=1},
                    new StudentCourses{StudentId=2,CourseId=2},
                    new StudentCourses{StudentId=2,CourseId=3},
                    new StudentCourses{StudentId=3,CourseId=1},
                    new StudentCourses{StudentId=3,CourseId=2},
                    new StudentCourses{StudentId=3,CourseId=3},
                };

            //add the seeding data to the entity HasData()
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<Course>().HasData(courses);
            modelBuilder.Entity<StudentCourses>().HasData(studentCourses);
        }
    }
}
