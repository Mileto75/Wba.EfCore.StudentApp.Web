using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class CoursesAddCourseViewModel
    {
        [Required(ErrorMessage ="Title required!")]
        public string Title { get; set; }
    }
}
