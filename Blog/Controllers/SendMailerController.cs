using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class SendMailerController : Controller
    {
        //
        // GET: /SendMailer/  
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ViewResult SendEmail(Blog.Models.MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {



                //Instância classe email
                MailMessage mail = new MailMessage();

                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.BlogEmail);
                mail.Subject = _objModelMail.Subject;
                mail.Body = _objModelMail.Name;
                mail.Body = _objModelMail.SenderEmail;
                string Body = "Nome do destinatário: " + _objModelMail.Name + "\n" +
                    "Remetente: " + _objModelMail.SenderEmail + " \n\n\n" + "Mensagem: " + _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;

                //Instância smtp do servidor, neste caso o gmail.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("flaviolandimblog@gmail.com", "*****");// Login e senha do e-mail.
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("/Views/SendMailer/SendOk.cshtml");

            }
            else
            {
                return View();
            }
        }
    }
}
