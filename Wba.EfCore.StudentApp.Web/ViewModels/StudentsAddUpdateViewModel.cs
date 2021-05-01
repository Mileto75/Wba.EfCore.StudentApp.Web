using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Web.Models;

namespace Wba.EfCore.StudentApp.Web.ViewModels
{
    public class StudentsAddUpdateViewModel
    {
        
        public long? Id { get; set; }
        [Required(ErrorMessage ="Firstname needed")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname needed")]
        public string Lastname { get; set; }
        //image
        public IFormFile Image { get; set; }
        //courses
        public List<CourseCheckbox> Courses { get; set; }
    }
}
