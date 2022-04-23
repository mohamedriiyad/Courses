using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Data;
using Courses.Models;
using Courses.ViewModels.PrerequisitesCourses;

namespace Courses.Controllers
{
    public class PrerequisitesCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrerequisitesCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrerequisitesCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PrerequisitesCourse
                .Include(p => p.Course)
                .Include(p => p.PreCourse)
                .Include(p => p.Course.Department);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: PrerequisitesCourses/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["PreCourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: PrerequisitesCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Create(PreCoursesVM prerequisitesCourse)
        {
            var result = false;
            string message;
            if (!ModelState.IsValid)
                return Json(result);
            
            var ids = prerequisitesCourse.tests.Select(t => t.Id).ToList();
            if (!ids.Contains(prerequisitesCourse.CourseId))
            {
                foreach (var id in ids)
                {
                    await _context.PrerequisitesCourse.AddAsync(new PrerequisitesCourse { PreCourseId = id, CourseId = prerequisitesCourse.CourseId });
                }
                await _context.SaveChangesAsync();

                result = true;
                message = "The Course was Added SUCCESSFULLY!";
                return Json(new { result, message });  
            }

            message = "The course shouldn't be itself PRECOURSE.";
            return Json(new { result, message });
        }
        private bool PrerequisitesCourseExists(int id)
        {
            return _context.PrerequisitesCourse.Any(e => e.CourseId == id);
        }
    }
}
