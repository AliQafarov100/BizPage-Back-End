using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using BizPage_Back_End.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizPage_Back_End.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            List<Client> clients = await _context.Clients.ToListAsync();
            List<Portfolio> portfolios = await _context.Portfolios.ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Address> addresses = await _context.Addresses.ToListAsync();
            List<Card> cards = await _context.Cards.ToListAsync();

            HomeVM model = new HomeVM
            {
                Sliders = sliders,
                Clients = clients,
                Portfolios = portfolios,
                Categories = categories,
                Addresses = addresses,
                Cards = cards
            };
            return View(model);
        }
    }
}
