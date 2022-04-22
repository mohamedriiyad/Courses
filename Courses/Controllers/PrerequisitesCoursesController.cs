using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Data;
using Courses.Models;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,PreCourseId")] PrerequisitesCourse prerequisitesCourse)
        {
            if (ModelState.IsValid)
            {
                if(prerequisitesCourse.CourseId != prerequisitesCourse.PreCourseId)
                {
                    _context.Add(prerequisitesCourse);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Courses");
                }
                ModelState.AddModelError(string.Empty, "The course shouldn't be itself preCourse.");

                ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
                ViewData["PreCourseId"] = new SelectList(_context.Courses, "Id", "Name");
                return View(prerequisitesCourse);
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", prerequisitesCourse.CourseId);
            ViewData["PreCourseId"] = new SelectList(_context.Courses, "Id", "Name", prerequisitesCourse.PreCourseId);
            return View(prerequisitesCourse);
        }
        private bool PrerequisitesCourseExists(int id)
        {
            return _context.PrerequisitesCourse.Any(e => e.CourseId == id);
        }
    }
}
