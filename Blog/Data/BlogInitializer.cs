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

            if (context.Biografias.Any())
            {
                return;
            }

            var biografias = new Biografia[]
            {
                new Biografia {Texto = "Biografia de texte"}
            };

            foreach (Biografia m in biografias)
            {
                context.Biografias.Add(m);
            }
            context.SaveChanges();
        }
    }
}
