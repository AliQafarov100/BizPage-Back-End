using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BizPage_Back_End.DAL;
using BizPage_Back_End.Models;
using BizPage_Back_End.Extensions;
using Microsoft.AspNetCore.Hosting;
using BizPage_Back_End.Utilities;

namespace BizPage_Back_End.Areas.BizAdmin.Controllers
{
    [Area("BizAdmin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlidersController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: BizAdmin/Sliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }

        // GET: BizAdmin/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: BizAdmin/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BizAdmin/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Slider slider)
        {
            if (ModelState.IsValid)
            {
                if(slider.Photo != null)
                {
                    if (slider.Photo.IsOkay(1))
                    {
                        slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Slider");
                        _context.Add(slider);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "File size must less than 1Mb and must image format");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("Photo", "Please choose image file");
                }
                
            }
            return View(slider);
        }

        // GET: BizAdmin/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: BizAdmin/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Slider slider)
        {
            Slider existed = await _context.Sliders.FindAsync(id);
            if (id != slider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (slider.Photo.IsSize(1))
                    {
                        

                        string path = _env.WebRootPath + @"assets\Image\Slider" + existed.Image;

                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        existed.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\Image\Slider");

                        existed.Title = slider.Title;
                        existed.SubTilte = slider.SubTilte;
                        existed.Order = slider.Order;

                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
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
            return View(slider);
        }

        // GET: BizAdmin/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                
                return NotFound();
            }

            string path = _env.WebRootPath + @"assets\Image\Slider" + slider.Image;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return View(slider);
        }

        // POST: BizAdmin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
            return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
