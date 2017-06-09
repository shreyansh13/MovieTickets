using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace BahubaliTickets
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputFile = @"E:\Code\Hacks\data\output.txt";
            Utils.ScrapeAmazon(outputFile);
            //Helper.GetTickets(args);
        }
    }

    class Helper
    {

        public static void GetTickets (string[] args)
        {
            if (args.Length != 5)
            {
                PrintTicketsHelp ();
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

        static void PrintTicketsHelp()
        {
            Console.WriteLine("{Date} {Theatres} {To MailingList} {From email address} {Password of From Email}");
        }

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
                status = Utils.SendMail(msg, aFromEmailAddress, aToEmailAddresses, aPassword);
            }
            return status;
        }


    }
}
