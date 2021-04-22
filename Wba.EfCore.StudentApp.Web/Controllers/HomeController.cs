using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Wba.EfCore.StudentApp.Domain.Entities;
using Wba.EfCore.StudentApp.Web.Data;
using Wba.EfCore.StudentApp.Web.Models;

namespace Wba.EfCore.StudentApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //dependency injection: declaration
        private readonly SchoolDbContext _schoolDbContext;
        //constructor injection
        public HomeController(SchoolDbContext schoolDbContext)
        {
            //injection 
            _schoolDbContext = schoolDbContext;
        }

        public IActionResult Index()
        {
            var courses = _schoolDbContext
                .Courses
                .ToList();
            var course = _schoolDbContext
                .Courses
                .Include(c => c.Teacher)
                .SingleOrDefault(c => c.Id == 1);

            var student = _schoolDbContext
                .Students
                .Include(s => s.Courses)
                .ThenInclude(c => c.Course)
                .ThenInclude(c => c.Teacher);
                
            Console.WriteLine(student);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
