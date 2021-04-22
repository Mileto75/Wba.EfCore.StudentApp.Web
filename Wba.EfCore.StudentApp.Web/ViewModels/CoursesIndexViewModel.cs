using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class CoursesIndexViewModel
    {
        public List<CoursesShowCourseInfoViewModel> 
            Courses { get; set; }
       
    }
}
