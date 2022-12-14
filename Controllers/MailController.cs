using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace MatrizPlanificacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailController : ControllerBase
    {
        [HttpPost(template: ApiRoutes.Mail.Notify)]
        public async Task<ActionResult> Notify()
        {
            

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ClienteCorreos@outlook.com", "MatizEcu_911");

            MailMessage correo = new MailMessage();

            correo.From = new MailAddress("ClienteCorreos@outlook.com");//Correo de salida
            correo.To.Add("mat_mb@hotmail.com"); //Correo destino?
            correo.Subject = "Correo de prueba"; //Asunto
            correo.Body = "Este es un correo de prueba desde c#";
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;

            client.Send(correo);
            //client.SendCompleted += SentMessageTrigger;


            return NotFound();
        }


    }
}


//SmtpClient smtp = new SmtpClient();
//MailMessage correo = new MailMessage();


//smtp.UseDefaultCredentials = false;
//smtp.Host = "outlook.office365.com"; //Host del servidor de correo
//smtp.Port = 995; //Puerto de salida
//smtp.Credentials = new System.Net.NetworkCredential("ClienteCorreos@outlook.com", "MatizEcu_911");//Cuenta de correo
//ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
////smtp.EnableSsl = true;//True si el servidor de correo permite ssl

//correo.From = new MailAddress("ClienteCorreos@outlook.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
//correo.To.Add("mat_mb@hotmail.com"); //Correo destino?
//correo.Subject = "Correo de prueba"; //Asunto
//correo.Body = "Este es un correo de prueba desde c#";
//correo.IsBodyHtml = true;
//correo.Priority = MailPriority.Normal;



//smtp.Send(correo);
