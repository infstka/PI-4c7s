using LR7_feed;
using System;
using System.ServiceModel.Web;

namespace LR7.Host
{
    class Program
    {
        static void Main()
        {
            using (WebServiceHost host = new WebServiceHost(typeof(FeedService)))
            {
                host.Open();
                Console.WriteLine($"Student Notes Feed Started: {DateTime.Now}");
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
