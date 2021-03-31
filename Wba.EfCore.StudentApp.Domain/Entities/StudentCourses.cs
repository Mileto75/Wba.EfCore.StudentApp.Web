using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.EfCore.StudentApp.Domain.Entities
{
    public class StudentCourses
    {
        //composite key(gecombineerde sleutel)
        public long StudentId { get; set; }
        public long CourseId { get; set; }
        //Navigation properties
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
