using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.DAL;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class MateriaController : Controller
    {
        private readonly BlogContext _context;
        private readonly MateriaDAL materiaDAL;
        

        public MateriaController(BlogContext context)
        {
            this._context = context;
            materiaDAL = new MateriaDAL(context);
          
        }
              

        public async Task<IActionResult> Index(string Pesquisa)
        {
            var materiasPesquisa = from m in _context.Materias select m;

            if (!String.IsNullOrEmpty(Pesquisa))
            {
                materiasPesquisa = materiasPesquisa.Where(s => s.Texto.Contains(Pesquisa));
            }

            return View(await materiasPesquisa.ToListAsync());
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Titulo, Autor, Resumo, Texto")] Materia materia, IFormFile foto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var stream = new MemoryStream();
                    await foto.CopyToAsync(stream);
                    materia.Foto = stream.ToArray();
                    materia.FotoMimeType = foto.ContentType;
                    await materiaDAL.GravarMateria(materia);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(materia);
        }


        public async Task<IActionResult> MateriaCompleta(long? id)
        {
            return await ObterMateriaVisaoPorId(id);
        }

        [Authorize]
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterMateriaVisaoPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(long? id, [Bind("MateriaID, Titulo, Autor, Resumo, Texto")]Materia materia, IFormFile foto, string chkRemoverFoto)
        {
            if(id != materia.MateriaID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var stream = new MemoryStream();
                if (chkRemoverFoto != null)
                {
                    materia.Foto = null;
                }
                else
                {
                    await foto.CopyToAsync(stream);
                    materia.Foto = stream.ToArray();
                    materia.FotoMimeType = foto.ContentType;
                }


                try
                {
                    
                    await materiaDAL.GravarMateria(materia);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await MateriaExists(materia.MateriaID))
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
            return View(materia);
        }

        [Authorize]
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterMateriaVisaoPorId(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var materia = await materiaDAL.EliminarMateriaPorId((long)id);
            TempData["Message"] = "Materia " + materia.Titulo.ToUpper() + " Foi Removida";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MateriaExists(long? id)
        {
            return await materiaDAL.ObterMateriaPorId((long)id) != null;

        }

        public async Task<IActionResult> ObterMateriaVisaoPorId(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materia = await materiaDAL.ObterMateriaPorId((long) id);

            if (materia == null)
            {
                return NotFound();
            }
            return View(materia);
        }

        public async Task<FileContentResult> GetFoto(long id)
        {
            Materia materia = await materiaDAL.ObterMateriaPorId(id);
            if (materia != null)
            {
                return File(materia.Foto, materia.FotoMimeType);
            }

            return null;
        }
    }
}
