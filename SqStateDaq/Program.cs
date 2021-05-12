using System;
using System.ServiceModel.Web;

namespace SqStateDaq
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://127.0.0.1:7788/");
            var serviceHost = new WebServiceHost(typeof(SqDataQueryServices), baseAddress);
            serviceHost.Open();
            Console.WriteLine("Web is running");
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            serviceHost.Close();

            Console.ReadLine();
        }
    }
}
