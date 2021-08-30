using System;
using System.ServiceModel.Web;

namespace SqStateDaq
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = new Uri("http://127.0.0.1:8080/");
            var serviceHost = new WebServiceHost(typeof(SqDataQueryServices), baseAddress);
            serviceHost.Open();
            Console.WriteLine("DAQ is running");
            Console.WriteLine("Enter `quit` any key to stop");
            while (Console.ReadLine() != "quit") { }
            serviceHost.Close();

            Console.WriteLine("DAQ stopped, press any key to exit");
            Console.ReadLine();
        }
    }
}
