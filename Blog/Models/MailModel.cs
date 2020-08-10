using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class MailModel
    {
        
        public string BlogEmail { get; set; }
        [Required]
        public string To { get; set; }
        [Required(ErrorMessage = "Oops! Eu preciso que você coloque seu nome!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Seu email é necessário para que eu possa entrar em contato com você ! =) ")]
        public string SenderEmail { get; set; }
        [Required(ErrorMessage = "Coloque um assunto ! =D")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Você precisa me dizer o que deseja conversar, digite uma mensagem ! ")]
        public string Body { get; set; }


        public MailModel()
        {
            BlogEmail = "flaviolandimblog@gmail.com";
            To = "fblandim@gmail.com";
        }
    }
}