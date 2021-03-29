using System;
using System.Collections.Generic;
using System.Text;

namespace Wba.EfCore.StudentApp.Domain.Entities
{
    public class Course
    {
        public long Id { get; set; }
        public string Title { get; set; }
        //navigation property
        public Teacher Teacher { get; set; }
        public long? TeacherId { get; set; }
    }
}
