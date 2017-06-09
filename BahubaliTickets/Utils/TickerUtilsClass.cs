using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using HtmlAgilityPack;

namespace TickerUtils
{
    public class TickerUtilsClass
    {
        public static void ReadSymbolData (string aSymbol)
        {
            string url = "http://finance.google.com/finance/info?client=ig&q=NSE:" + aSymbol;
            string jsonResponse = GET(url);
            string symbol = ParseJson(jsonResponse);
            Console.WriteLine("Symbol " + symbol);
        }

        public static void GetYearLowHigh (string aUrl)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(aUrl);
            //var findclasses = document.DocumentNode
            //        .Descendants("snap-panel-and-plusone")
            //        .Where(d =>
            //        d.Attributes.Contains("data-snapfield")
            //        &&
            //        d.Attributes["data-snapfield"].Value.Contains("range_52week"));

            var tmpParentdivNodes = document.DocumentNode.SelectNodes("//div[@id='elastic']");
            HtmlNode ParentdivNodes = tmpParentdivNodes.First();
            HtmlNode[] aTableNodes = ParentdivNodes.SelectNodes(".//table").ToArray();
            Console.WriteLine("Hello there");            
        }

        public static string ParseJson (string aJsonResponse)
        {
            if (aJsonResponse.StartsWith("\n//"))
                aJsonResponse = aJsonResponse.Replace("\n//", "");
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(aJsonResponse);
            string symbol = data[0].t;
            return symbol;
        }

        public static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();                    
                }
                throw;
            }
        }

    }
}
