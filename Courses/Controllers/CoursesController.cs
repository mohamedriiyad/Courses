using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Courses.Data;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;

namespace Courses.Controllers
{
    [Authorize(Roles = "admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courses
                .Include(p => p.Department)
                .Include(p => p.Prerequisites)
                .ThenInclude(pc =>pc.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses
        public async Task<IActionResult> AdminIndex()
        {
            var applicationDbContext = _context.Courses
                .Include(p => p.Department)
                .Include(p => p.Prerequisites)
                .ThenInclude(pc => pc.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.PrerequisitesCourse
                .Include(p => p.PreCourse)
                .Include(p => p.Course)
                .Include(p => p.Course.Department)
                .FirstOrDefaultAsync(m => m.Course.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["PreCourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Grade,Name,Credit,DepartmentId")] Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
                return View(course);
            }
            var courseInDb = await _context.Courses.FirstOrDefaultAsync(c => c.Code == course.Code);
            if (courseInDb != null) {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
                ModelState.AddModelError("", "This Course code Already exists.");
                return View(course);
            }
            _context.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "PrerequisitesCourses");
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Grade,Name,Credit,DepartmentId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Create", "PrerequisitesCourses");
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var result = false;
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return Json(result);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            result = true;
            return Json(result);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
