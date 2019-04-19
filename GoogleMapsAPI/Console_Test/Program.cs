using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Console_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string test =  Google_Maps_API.APICalls.GetJsonResultFromCall("Automation Anywhere 633 River Oaks Pkwy, San Jose", "AIzaSyCr4JB8XHkZRKnD829eS8KozznY_zVVWRc");
            string streetNumber = Google_Maps_API.APICalls.GetStreetNumber(test);
            string route = Google_Maps_API.APICalls.GetRoute(test);
            string city = Google_Maps_API.APICalls.GetCity(test);
            string postalCode = Google_Maps_API.APICalls.GetPostalCode(test);
            string country = Google_Maps_API.APICalls.GetCountry(test);
            Console.WriteLine(test);
        }
    }
}
