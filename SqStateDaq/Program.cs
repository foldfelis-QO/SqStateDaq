using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Automation.BDaq;

namespace SqStateDaq
{
    class Program
    {
        static void Main(string[] args)
        {
            // try
            // {
            //     var baseAddress = new Uri("http://127.0.0.1:7788/");
            //     using (var serviceHost = new ServiceHost(typeof(SqDataQueryServices), baseAddress))
            //     {
            //         var binding = new WebHttpBinding
            //         {
            //             TransferMode = TransferMode.Buffered,
            //             MaxBufferSize = 2147483647,
            //             MaxReceivedMessageSize = 2147483647,
            //             MaxBufferPoolSize = 2147483647,
            //             ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
            //             Security = { Mode = WebHttpSecurityMode.None }
            //         };
            //         serviceHost.AddServiceEndpoint(typeof(IDataQuery), binding, baseAddress);
            //         serviceHost.Opened += delegate
            //         {
            //             Console.WriteLine("Web is running");
            //         };
            //         serviceHost.Open();
            //         Console.WriteLine("Press any key to stop");
            //         Console.ReadKey();
            //         serviceHost.Close();
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine("Web service failed to start：{0}\n{1}", ex.Message, ex.StackTrace);
            // }

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
