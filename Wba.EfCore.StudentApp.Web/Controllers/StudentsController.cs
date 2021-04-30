using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;
using Wba.EfCore.StudentApp.Web.Data;
using Wba.EfCore.StudentApp.Web.Models;
using Wba.EfCore.StudentApp.Web.Services;
using Wba.EfCore.StudentApp.Web.ViewModels;

namespace Wba.EfCore.StudentApp.Web.Controllers
{
    
    
    public class StudentsController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;
        //hostingenvironment
        private readonly IHostingEnvironment _hostingEnvironment;
        //inject filemanagerservice class 
        private readonly IFileManagerService _fileManagerService;

        public StudentsController(SchoolDbContext schoolDbContext,
            IHostingEnvironment hostingEnvironment,
            IFileManagerService fileManagerService)
        {
            //inject schoolDb
            _schoolDbContext = schoolDbContext;
            _hostingEnvironment = hostingEnvironment;
            _fileManagerService = fileManagerService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //show a list of students
            StudentsIndexViewModel studentsIndexViewModel
                = new StudentsIndexViewModel();
            studentsIndexViewModel.Students = new List<StudentsShowInfoViewModel>();
            foreach(var student in await _schoolDbContext
                .Students
                .ToListAsync())
            {
                studentsIndexViewModel.Students
                    .Add
                    (
                        new StudentsShowInfoViewModel
                        {
                            Id=student.Id,
                            Name = $"{student.Firstname} {student.Lastname}",
                            Image = student.Image
                        }
                    );
            }
            return View(studentsIndexViewModel);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(StudentsAddUpdateViewModel
            studentsAddUpdateViewModel)
        {
            //save the student
            var student = new Student();
            student.Firstname = studentsAddUpdateViewModel.Firstname;
            student.Lastname = studentsAddUpdateViewModel.Lastname;
            
            //store the filename in database
            student.Image = 
                await _fileManagerService.SaveFile(studentsAddUpdateViewModel.Image
                , _hostingEnvironment.WebRootPath);
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

        [HttpGet]
        public IActionResult ConfirmDelete(long id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            var student = await _schoolDbContext
                .Students
                .FirstOrDefaultAsync(s => s.Id == id);
            _fileManagerService
                .DeleteFile(student.Image, _hostingEnvironment.WebRootPath);
            _schoolDbContext.Students.Remove(student);
            try
            {
                await _schoolDbContext.SaveChangesAsync();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index","Students");
        }

        [HttpGet]
        public async Task<IActionResult> ShowInfo(long id)
        {
            StudentsShowInfoViewModel studentsShowInfoViewModel
                = new StudentsShowInfoViewModel();
            var student = await _schoolDbContext
                .Students
                .Include(s => s.Courses)
                .ThenInclude(c =>c.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
            studentsShowInfoViewModel.Name = $"{student.Firstname} {student.Lastname}";
            studentsShowInfoViewModel.Image = student.Image;
            studentsShowInfoViewModel.CoursesTitles = student.Courses.Select(c => c.Course.Title);
            studentsShowInfoViewModel.CoursesIds = student.Courses.Select(c => c.Course.Id);
            return View(studentsShowInfoViewModel);
        }
    }
}
