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
    public class EnrollementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrollements
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enrollements.Include(e => e.ApplicationUser).Include(e => e.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Enrollements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements
                .Include(e => e.ApplicationUser)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollement == null)
            {
                return NotFound();
            }

            return View(enrollement);
        }

        // GET: Enrollements/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Grade");
            return View();
        }

        // POST: Enrollements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,CourseId,Grade")] Enrollement enrollement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", enrollement.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Grade", enrollement.CourseId);
            return View(enrollement);
        }

        // GET: Enrollements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements.FindAsync(id);
            if (enrollement == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", enrollement.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Grade", enrollement.CourseId);
            return View(enrollement);
        }

        // POST: Enrollements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,CourseId,Grade")] Enrollement enrollement)
        {
            if (id != enrollement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollementExists(enrollement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "UserName", enrollement.ApplicationUserId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Grade", enrollement.CourseId);
            return View(enrollement);
        }

        // GET: Enrollements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollement = await _context.Enrollements
                .Include(e => e.ApplicationUser)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollement == null)
            {
                return NotFound();
            }

            return View(enrollement);
        }

        // POST: Enrollements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollement = await _context.Enrollements.FindAsync(id);
            _context.Enrollements.Remove(enrollement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult EnrollementPage()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Grade");
            var courses = _context.Courses
                .Include(p => p.Department)
                .Include(p => p.Prerequisites)
                .ThenInclude(pc => pc.Course).ToList();
            
            return View(courses);
        }

        public ActionResult ConfirmedEnrollementPage()
        {
            return View();
        }
        private bool EnrollementExists(int id)
        {
            return _context.Enrollements.Any(e => e.Id == id);
        }
    }
}
