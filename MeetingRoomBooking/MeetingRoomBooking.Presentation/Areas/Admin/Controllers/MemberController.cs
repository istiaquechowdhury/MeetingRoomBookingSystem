using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MeetingRoomBooking.DataAccess.Identity;
using MeetingRoomBooking.Presentation.Areas.Admin.Models;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class MemberController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        // GET: Display User List
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserManagementViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserManagementViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pin = user.Pin,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Department = user.Department,
                    Designation = user.Designation,
                    Status = user.Status,
                    Roles = roles
                });
            }

            return View(userList);
        }

        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(new UserManagementViewModel());
        }

        // POST: Create User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    Department = model.Department,
                    Designation = model.Designation,
                    Status = model.Status
                };

                var result = await _userManager.CreateAsync(user, "Default@123"); // Default password
                if (result.Succeeded)
                {
                    if (model.Roles != null && model.Roles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, model.Roles);
                    }

                    TempData["success"] = "User created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(model);
        }


        // GET: Edit User Form
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new UserManagementViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Department = user.Department,
                Designation = user.Designation,
                Status = user.Status,
                Roles = roles
            };

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        // POST: Edit User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserManagementViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user == null) return NotFound();

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Department = model.Department;
                user.Designation = model.Designation;
                user.Status = model.Status;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var existingRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, existingRoles);
                    if (model.Roles != null && model.Roles.Any())
                    {
                        await _userManager.AddToRolesAsync(user, model.Roles);
                    }

                    TempData["success"] = "User updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            ViewBag.Roles = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View(model);
        }

        // POST: Delete User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["success"] = "User deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "Failed to delete user.";
            return RedirectToAction(nameof(Index));
        }
    }



}

