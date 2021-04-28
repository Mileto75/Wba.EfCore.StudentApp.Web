using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.Models
{
    public class CourseCheckbox
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }
}
