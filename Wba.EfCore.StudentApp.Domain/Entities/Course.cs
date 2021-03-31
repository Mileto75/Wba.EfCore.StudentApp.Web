using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wba.EfCore.StudentApp.Domain.Entities
{
    public class Course
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        //navigation property
        public Teacher Teacher { get; set; }
        public long? TeacherId{ get; set; }//foreign key
        public ICollection<StudentCourses> Students { get; set; }
    }
}
