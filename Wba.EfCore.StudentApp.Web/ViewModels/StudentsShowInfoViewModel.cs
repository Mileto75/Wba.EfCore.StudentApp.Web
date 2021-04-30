using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class StudentsShowInfoViewModel
    {
        public long  Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public IEnumerable<string> CoursesTitles { get; set; }
        public IEnumerable<long> CoursesIds { get; set; }
    }
}
