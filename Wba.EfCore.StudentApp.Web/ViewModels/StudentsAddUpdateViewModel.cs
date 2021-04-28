using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Web.Models;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class StudentsAddUpdateViewModel
    {
        
        public long? Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //image
        public IFormFile Image { get; set; }
        //courses
        public List<CourseCheckbox> Courses { get; set; }
    }
}
