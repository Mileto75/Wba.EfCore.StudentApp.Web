using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.EfCore.StudentApp.Domain.Entities
{
    public class Student
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<StudentCourses> Courses { get; set; }
    }
}
