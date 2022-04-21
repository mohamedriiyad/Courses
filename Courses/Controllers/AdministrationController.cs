﻿using Courses.Data;
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

        // GET: AdministrationController/Details/5
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
                Universities = universities
            };

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserSelfEditVM input)
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
