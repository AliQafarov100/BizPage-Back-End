using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizPage_Back_End.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizPage_Back_End.Areas.BizAdmin.Controllers
{
    [Area("BizAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _manager;

        public UserController(UserManager<AppUser> manager)
        {
            _manager = manager;
        }
        public async Task<IActionResult> Users()
        {
            List<AppUser> users = await _manager.Users.ToListAsync();
            return View(users);
        }
    }
}
