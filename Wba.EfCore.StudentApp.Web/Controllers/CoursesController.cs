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
            CoursesAddUpdateCourseViewModel coursesAddCourseViewModel
                = new CoursesAddUpdateCourseViewModel();
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
        [HttpGet]
        public IActionResult Update(long Id)
        {
            //get the course to update
            var course = _schoolDbContext
                .Courses
                .FirstOrDefault(c => c.Id == Id);
            //declare viewModel
            CoursesAddUpdateCourseViewModel
                coursesAddUpdateCourseViewModel = 
                new CoursesAddUpdateCourseViewModel();
            coursesAddUpdateCourseViewModel
                .Title = course?.Title;
            coursesAddUpdateCourseViewModel
                .TeacherId = course?.TeacherId;
            coursesAddUpdateCourseViewModel
                .CourseId = course.Id;
            coursesAddUpdateCourseViewModel
                .Teachers = new List<SelectListItem>();
            foreach(var teacher in _schoolDbContext.Teachers.ToList())
            {
                coursesAddUpdateCourseViewModel
                    .Teachers.Add(new SelectListItem
                    {
                        Text = $"{teacher.Firstname} {teacher.Lastname}"
                    ,
                        Value = $"{teacher.Id}"
                    });
            }
            return View(coursesAddUpdateCourseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(CoursesAddUpdateCourseViewModel
            coursesAddUpdateCourseViewModel)
        {
            if(!ModelState.IsValid)
            {
                //fill the teachers
                coursesAddUpdateCourseViewModel
                .Teachers = new List<SelectListItem>();
                foreach (var teacher in _schoolDbContext.Teachers.ToList())
                {
                    coursesAddUpdateCourseViewModel
                        .Teachers.Add(new SelectListItem
                        {
                            Text = $"{teacher.Firstname} {teacher.Lastname}"
                        ,
                            Value = $"{teacher.Id}"
                        });
                }
                return View(coursesAddUpdateCourseViewModel);
            }
            //update course
            var course = _schoolDbContext
                .Courses
                .FirstOrDefault(c => c.Id ==
                coursesAddUpdateCourseViewModel.CourseId);
            course.Title = coursesAddUpdateCourseViewModel
                .Title;
            course.TeacherId = coursesAddUpdateCourseViewModel
                .TeacherId;
            
            try
            {
                _schoolDbContext.SaveChanges();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            //redirect to course list
            return RedirectToAction("Index", "Courses");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourse(CoursesAddUpdateCourseViewModel
            coursesAddCourseViewModel)
        {
            if(!ModelState.IsValid)
            {
                coursesAddCourseViewModel.Teachers =
                    new List<SelectListItem>();
                //loop over teachers
                foreach (var teacher in
                    _schoolDbContext.Teachers.ToList())
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
