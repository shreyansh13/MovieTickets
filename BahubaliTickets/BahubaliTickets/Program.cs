using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BahubaliTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                PrintHelp();
            }
            else
            {
                string url = "https://in.bookmyshow.com/buytickets/baahubali-2-the-conclusion-hindi-hyderabad/movie-hyd-ET00050679-MT/";
                string date = args[0];
                string[] theatres = args[1].Split(',');
                string[] toEmails = args[2].Split(',');
                string fromEmail = args[3];
                string password = args[4];
                bool status = false;
                do
                {
                    status = Helper.CheckAndMail(url, theatres, date, fromEmail, toEmails, password);
                } while (status == false);
            }
        }

        static void PrintHelp ()
        {
            Console.WriteLine("{Date} {Theatres} {To MailingList} {From email address} {Password of From Email}");
        }
    }

    class Helper
    {
        public static bool CheckAndMail(string aUrl, string[] aTheatres, string aDate, string aFromEmailAddress, string[] aToEmailAddresses, string aPassword)
        {
            bool status = false;
            string msg = "Tickets for Date : "  + aDate + " Available Now in : ";
            string finalUrl = aUrl + aDate.Replace("/","");
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(finalUrl);
                foreach (string theatre in aTheatres)
                {
                    if (htmlCode.Contains(theatre))
                        msg = msg + theatre + ", " +  "\n";
                }
            }            

            if (!String.IsNullOrEmpty(msg))
            {                
                status = SendMail(msg, aFromEmailAddress, aToEmailAddresses, aPassword);
            }
            return status;
        }

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
    }
}
