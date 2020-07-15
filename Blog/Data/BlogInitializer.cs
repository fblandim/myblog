using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Data
{
    public class BlogInitializer
    {

        public static void Initialize(BlogContext context)
        {
            context.Database.EnsureCreated();

            if (context.Materias.Any())
            {
                return;
            }

            var materias = new Materia[]
            {
                new Materia {Titulo = "Titulo de teste", Texto = "Texto de teste"}
            };

            foreach (Materia m in materias)
            {
                context.Materias.Add(m);
            }
            context.SaveChanges();
        }
    }
}
