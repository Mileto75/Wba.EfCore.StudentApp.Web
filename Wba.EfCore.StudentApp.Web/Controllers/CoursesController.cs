using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;
using Wba.EfCore.StudentApp.Web.Data;
using Wba.EfCore.StudentApp.Web.ViewModels;

namespace Wba.EfCore.StudentApp.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDbContext _schoolDbContext;

        public CoursesController(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            CoursesIndexViewModel coursesIndexViewModel
                = new CoursesIndexViewModel();
            coursesIndexViewModel.Courses 
                = new List<CoursesShowCourseInfoViewModel>();
            foreach(var course in _schoolDbContext
                .Courses
                .Include(c => c.Teacher).ToList())
            {
                coursesIndexViewModel.Courses.Add
                (
                    new CoursesShowCourseInfoViewModel
                    { Id = course.Id,Title=course.Title,
                    TeacherName = $"{course.Teacher.Firstname} {course.Teacher.Lastname}"}
                );
            }
            return View(coursesIndexViewModel);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            Course newCourse = new Course();
            newCourse.Title = "CIA";
            newCourse.TeacherId = 1;
            //add to context
            _schoolDbContext
                .Courses
                .Add(newCourse);
            //editeren
            try
            {
                _schoolDbContext.SaveChanges();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditCourse(long Id)
        {
            var course = _schoolDbContext
                .Courses
                .FirstOrDefault(c => c.Id == Id);
            //edit
            course.Title = "CIB";
            //save to db
            try
            {
                _schoolDbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return RedirectToAction("Index", "Courses");
        }
        [HttpGet]
        public IActionResult ShowCourseInfo(long Id)
        {
            var course = _schoolDbContext
                .Courses
                .Include(c =>c.Teacher)
                .FirstOrDefault(c => c.Id == Id);
            //viewModel
            CoursesShowCourseInfoViewModel
                coursesShowCourseInfoViewModel = new CoursesShowCourseInfoViewModel();
            //fill the model
            coursesShowCourseInfoViewModel.Title = course.Title;
            coursesShowCourseInfoViewModel.TeacherName
                = $"{course.Teacher.Firstname} {course.Teacher.Lastname}";
            return View(coursesShowCourseInfoViewModel);
        }

        [HttpGet]
        public IActionResult ConfirmDelete(long Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        [HttpGet]
        public IActionResult Delete(long Id)
        {
            var course = _schoolDbContext
                .Courses
                .FirstOrDefault(c => c.Id == Id);
            //Change tracking updaten
            _schoolDbContext.Courses.Remove(course);
            //save to db
            try
            {
                _schoolDbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Courses");
        }
    }
}
