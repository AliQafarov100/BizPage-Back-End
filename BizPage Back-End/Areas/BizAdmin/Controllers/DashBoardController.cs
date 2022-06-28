using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BizPage_Back_End.Areas.BizAdmin.Controllers
{
    [Area("BizAdmin")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
