using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    TeacherName = 
                    $"{course?.Teacher?.Firstname} {course?.Teacher?.Lastname}"}
                );
            }
            return View(coursesIndexViewModel);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            //loads the form
            //viewModel
            CoursesAddCourseViewModel coursesAddCourseViewModel
                = new CoursesAddCourseViewModel();
            coursesAddCourseViewModel.Teachers 
                = new List<SelectListItem>();
            //loop over teachers
            foreach(var teacher in _schoolDbContext.Teachers.ToList())
            {
                //add teachers to list
                coursesAddCourseViewModel.Teachers
                    .Add(new SelectListItem 
                    {Text=$"{teacher.Firstname} {teacher.Lastname}"
                    ,Value=$"{teacher.Id}"});
            }
            return View(coursesAddCourseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(CoursesAddCourseViewModel
            coursesAddCourseViewModel)
        {
            if(!ModelState.IsValid)
            {
                coursesAddCourseViewModel.Teachers = new List<SelectListItem>();
                //loop over teachers
                foreach (var teacher in _schoolDbContext.Teachers.ToList())
                {
                    //add teachers to list
                    coursesAddCourseViewModel.Teachers
                        .Add(new SelectListItem
                        {
                            Text = $"{teacher.Firstname} {teacher.Lastname}"
                        ,
                            Value = $"{teacher.Id}"
                        });
                }
                return View(coursesAddCourseViewModel);
            }
            //save new course
            Course newCourse = new Course();
            newCourse.Title = coursesAddCourseViewModel.Title;
            //teacher
            newCourse.TeacherId = coursesAddCourseViewModel.TeacherId;
            _schoolDbContext.Courses.Add(newCourse);
            try 
            {
                _schoolDbContext.SaveChanges();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
            //redirect to index
            return RedirectToAction("Index","Courses");
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
