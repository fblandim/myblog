using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data.DAL
{
    public class MateriaDAL
    {
        private BlogContext _context;

        public MateriaDAL(BlogContext context)
        {
            _context = context;
        }

        public IQueryable<Materia> ObterMateriaClassificadaPorId()
        {
            return _context.Materias.OrderBy(m => m.MateriaID);
        } 

        public async Task<Materia> ObterMateriaPorId(long id)
        {
            return await _context.Materias.SingleOrDefaultAsync(m => m.MateriaID == id);

        }

        public async Task<Materia> GravarMateria(Materia materia)
        {
            if (materia.MateriaID == null)
            {
                _context.Materias.Add(materia);
            }
            else
            {
                _context.Update(materia);
            }
            await _context.SaveChangesAsync();
            return materia;
        }

        public async Task<Materia> EliminarMateriaPorId(long id)
        {
            Materia materia = await ObterMateriaPorId(id);
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            return materia;
        }

        



    }
}
