using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizPage_Back_End.Models;
using BizPage_Back_End.Utilities;
using BizPage_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BizPage_Back_End.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signIn;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> manager,SignInManager<AppUser> signIn,RoleManager<IdentityRole> roleManager)
        {
            _manager = manager;
            _signIn = signIn;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser user = new AppUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Username,
                Email = register.Email
            };
            if(register.Term == true)
            {
                IdentityResult result = await _manager.CreateAsync(user, register.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "You cannot registered without our agree!");
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _manager.FindByNameAsync(login.Username);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signIn.PasswordSignInAsync(user, login.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password or username");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);

            EditVM edit = new EditVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email
            };

            return View(edit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Edit(EditVM editUser)
        {
            AppUser existed = await _manager.FindByNameAsync(User.Identity.Name);

            EditVM editVM = new EditVM
            {
                FirstName = editUser.FirstName,
                LastName = editUser.LastName,
                Username = editUser.Username
            };

            if (!ModelState.IsValid) return View(editVM);

            bool cases = editVM.Password == null && editVM.ConfirmPassword == null && editVM.CurrentPassword != null;

            if(editVM.Email != null || editUser.Email != existed.Email)
            {
                ModelState.AddModelError("", "Email cannot changed");
                return View(editVM);
            }

            if (cases)
            {
                existed.UserName = editUser.Username;
                existed.FirstName = editUser.FirstName;
                existed.LastName = editUser.LastName;
                await _manager.UpdateAsync(existed);
            }
            else
            {
                existed.UserName = editUser.Username;
                existed.FirstName = editUser.FirstName;
                existed.LastName = editUser.LastName;

                IdentityResult result = await _manager.ChangePasswordAsync(existed, editUser.CurrentPassword, editUser.Password);

                if (!result.Succeeded)
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(editVM);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Member.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await _roleManager.CreateAsync(new IdentityRole { Name = Roles.SuperAdmin.ToString() });
        }
    }
}
