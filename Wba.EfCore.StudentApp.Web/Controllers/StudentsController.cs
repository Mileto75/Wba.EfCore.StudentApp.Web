using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;
using Wba.EfCore.StudentApp.Web.Data;
using Wba.EfCore.StudentApp.Web.Models;
using Wba.EfCore.StudentApp.Web.ViewModels;

namespace Wba.EfCore.StudentApp.Web.Controllers
{
    
    
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;
        //hostingenvironment
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentsController(SchoolDbContext schoolDbContext,
            IHostingEnvironment hostingEnvironment)
        {
            //inject schoolDb
            _schoolDbContext = schoolDbContext;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //show a list of students
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //show student add form
            StudentsAddUpdateViewModel studentsAddUpdateViewModel
                = new StudentsAddUpdateViewModel();
            //load the courses async
            var courses = await _schoolDbContext
                .Courses
                .ToListAsync();
            //loop over courses and fill list of checkboxes
            studentsAddUpdateViewModel.Courses
                = new List<CourseCheckbox>();
            foreach(var course in courses)
            {
                studentsAddUpdateViewModel.Courses
                    .Add(
                    new CourseCheckbox
                    {
                        Id = course.Id,
                        Name = course.Title,
                    }
                    );
            }
            return View(studentsAddUpdateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentsAddUpdateViewModel
            studentsAddUpdateViewModel)
        {
            //save the student
            var student = new Student();
            student.Firstname = studentsAddUpdateViewModel.Firstname;
            student.Lastname = studentsAddUpdateViewModel.Lastname;
            //image
            //create the path wwwroot/images/filename.jpg
            //make a filename
            var fileName = $"{Guid.NewGuid()}_{studentsAddUpdateViewModel.Image.FileName}";
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath
                ,"images",fileName
                );
            //store on disk
            FileStream stream
                = new FileStream(filePath, FileMode.Create);
            //copy from viewmodel file
            await studentsAddUpdateViewModel.Image.CopyToAsync(stream);
            stream.Dispose();
            //store the filename in database
            student.Image = fileName;
            //add courses => Many to many relatie
            //loop over the checkbox list
            student.Courses = new List<StudentCourses>();
            foreach (var course in studentsAddUpdateViewModel
                .Courses
                .Where(c => c.Selected == true))
            {
                //add studentCourses entity
                //toevoegen aan courses
                student.Courses.Add
                (
                    new StudentCourses { Student = student,
                    CourseId=course.Id}
                );
            }

            _schoolDbContext.Students.Add(student);
            await _schoolDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Students");
        }
    }
}
