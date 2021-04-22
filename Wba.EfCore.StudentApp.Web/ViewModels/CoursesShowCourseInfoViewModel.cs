using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class CoursesShowCourseInfoViewModel
    {
        public long Id { get; set; }
        public string Title  { get; set; }
        public string TeacherName { get; set; }
    }
}
