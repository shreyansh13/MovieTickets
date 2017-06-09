using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BahubaliTickets
{
    class Utils
    {
        public static bool SendMail(string aMessage, string aFromEmailAddress, string[] aToEmailAddress, string aPassword)
        {
            bool status = false;
            MailMessage mail = new MailMessage();

            foreach (string toemail in aToEmailAddress)
                mail.To.Add(toemail);
            mail.From = new MailAddress(aFromEmailAddress);
            mail.Subject = "!!! Tickets Available Now !!! Hurry !!!";

            mail.Body = aMessage;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential
                 (aFromEmailAddress, aPassword); // ***use valid credentials***
            smtp.Port = 587;

            //Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);
            Console.WriteLine("Mail Sent");
            status = true;
            return status;
        }

        public static void ScrapeAmazon (string aOutputFile)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("http://www.amazon.in/b/ref=gbph_ftr_m-2_c49a_page_4?ie=UTF8&node=5731634031&pf_rd_p=ab10557d-150a-45d0-8b1e-d768e4c9c49a&pf_rd_r=1VGTH5DH6PJY83PV478Z&gb_f_Summer_Blockbuster_PC=dealStates:AVAILABLE%252CWAITLIST%252CWAITLISTFULL,dealTypes:LIGHTNING_DEAL,page:4,sortOrder:BY_SCORE,dealsPerPage:1000&pf_rd_s=merchandised-search-2&pf_rd_t=101&pf_rd_i=5731634031&pf_rd_m=A1VBAL9TL5WCBF");
            var findclasses = document.DocumentNode
    .Descendants("span")
    .Where(d =>
        d.Attributes.Contains("class")
        &&
        d.Attributes["class"].Value.Contains("dealContainer")    
    );

            Array nodes = findclasses.ToArray();
        }
    }
}
