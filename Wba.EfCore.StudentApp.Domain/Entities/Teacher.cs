using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wba.EfCore.StudentApp.Domain.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        //1 teacher heeft meerdere Courses
        public ICollection<Course> Courses { get; set; }
    }
}
