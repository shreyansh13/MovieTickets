using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ticker
{
    class Program
    {
        static void Main(string[] args)
        {
            //TickerUtils.TickerUtilsClass.ReadSymbolData("HDFC");
            TickerUtils.TickerUtilsClass.GetYearLowHigh("https://www.google.com/finance?q=NSE:HDFC");
            
        }
    }
}
