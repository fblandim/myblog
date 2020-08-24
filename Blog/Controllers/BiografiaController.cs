using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.DAL;
using Blog.Migrations;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Biografia = Blog.Models.Biografia;

namespace Blog.Controllers
{
    public class BiografiaController : Controller
    {

        private readonly BlogContext _context;


        public BiografiaController(BlogContext context)
        {
            _context = context;

        }



        public async Task<IActionResult> Index()
        {
            return View(await _context.Biografias.OrderBy(c => c.BiografiaId).ToListAsync());
        }


        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biografia = await _context.Biografias.SingleOrDefaultAsync(m => m.BiografiaId == id);

            if (biografia == null)
            {
                return NotFound();
            }

            ViewBag.Biografias = new SelectList(_context.Biografias.OrderBy(b => b.Texto), "BiografiaId", "Texto", biografia.BiografiaId);

            return View(biografia);




        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("BiografiaId, Texto")] Biografia biografia)
        {
            if (id != biografia.BiografiaId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biografia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BiografiaExists(biografia.BiografiaId))
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
            return View(biografia);
        }


        private bool BiografiaExists(long? id)
        {
            return _context.Biografias.Any(e => e.BiografiaId == id);
        }



    }
}
