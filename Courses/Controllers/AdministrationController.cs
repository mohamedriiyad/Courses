using Courses.Data;
using Courses.Models;
using Courses.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Courses.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public AdministrationController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: AdministrationController
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = _userManager.Users.Select(u => new UserListVM
            {
                UserName = u.UserName,
                Email = u.Email,
                Id = u.Id,
                PhoneNumber = u.PhoneNumber,
                University = u.University.Name
            });

            return View(users);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var universities = _db.Universities.ToList();
            var user = new UserInputVM { Universities = universities };

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(UserInputVM input)
        {
            var university = await _db.Universities.FindAsync(input.UniversityId);
            if (university == null)
                ModelState.AddModelError(string.Empty, "University Field is required.");
            if (ModelState.IsValid && university != null)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Username,
                    Email = input.Email,
                    UniversityId = input.UniversityId,
                    PhoneNumber = input.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            input.Universities = _db.Universities.ToList();
            // If we got this far, something failed, redisplay form
            return View(input);
        }
        // GET: AdministrationController/Details/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Details(string id)
        {
            var userInDb = await FindByIdAsync(id);
            if(userInDb == null)
                return BadRequest("There is no such USER!");

            var user = new UserVM
            {
                UserName = userInDb.UserName,
                Email = userInDb.Email,
                PhoneNumber = userInDb.PhoneNumber,
                University = userInDb.University.Name
            };

            return View(user);
        }


        // GET: AdministrationController/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var userInDb = await FindByIdAsync(id);
            if(userInDb == null)
                return BadRequest("There is no such USER!");

            var user = new UserVM
            {
                Id = userInDb.Id,
                UserName = userInDb.UserName,
                Email = userInDb.Email
            };

            return View(user);
        }

        // POST: AdministrationController/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<JsonResult> DeleteConfirmed(string id)
        {
            var result = false;
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                result = true;
                await _userManager.DeleteAsync(user);
            }

            return Json(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Edit(string id)
        {
            var userInDb = await FindByIdAsync(id);
            if (userInDb == null)
                return BadRequest("There is no such USER!");
            var universities = _db.Universities.ToList();
            var user = new UserEditVM
            {
                Id = userInDb.Id,
                Email = userInDb.Email,
                PhoneNumber = userInDb.PhoneNumber,
                Universities = universities
            };

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(UserSelfEditVM input)
        {
            if (!ModelState.IsValid)
            {
                input.Universities = _db.Universities.ToList();
                return View(input);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with Username '{_userManager.GetUserName(User)}'.");
            }
            var userInDb = await _db.Users.FindAsync(user.Id);
            userInDb.Email = input.Email;
            userInDb.PhoneNumber = input.PhoneNumber;
            userInDb.UniversityId = input.UniversityId;
            await _db.SaveChangesAsync();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, input.NewPassword); 
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                input.Universities = _db.Universities.ToList();
                return View(input);
            }


            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> UserEdit(string name)
        {
            var userInDb = await _userManager.FindByNameAsync(name);
            if (userInDb == null)
                return BadRequest("There is no such USER!");
            var universities = _db.Universities.ToList();
            var user = new UserSelfEditVM
            {
                Id = userInDb.Id,
                Email = userInDb.Email,
                PhoneNumber = userInDb.PhoneNumber,
                NewGPA = userInDb.GPA,
                Universities = universities
            };

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserEdit(UserSelfEditVM input)
        {

            var university = await _db.Universities.FindAsync(input.UniversityId);
            if (university == null)
            {
                ModelState.AddModelError(string.Empty, "University Field is required.");
                input.Universities = _db.Universities.ToList();
                return View(input);
            }
            
            if (!ModelState.IsValid)
            {
                input.Universities = _db.Universities.ToList();
                return View(input);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with Username '{_userManager.GetUserName(User)}'.");
            }
            var userInDb = await _db.Users.FindAsync(user.Id);
            userInDb.Email = input.Email;
            userInDb.PhoneNumber = input.PhoneNumber;
            userInDb.UniversityId = input.UniversityId;
            await _db.SaveChangesAsync();
            
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                input.Universities = _db.Universities.ToList();
                return View(input);
            }


            return RedirectToAction("Index", "Home");
        }
        private async Task<ApplicationUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
    }
}
