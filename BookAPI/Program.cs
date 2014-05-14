using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace BookAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:31337");
            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
  
            var server = new HttpSelfHostServer(config);

            var task = server.OpenAsync();
            task.Wait();

            Console.WriteLine("Server Up");
            Console.ReadLine();

        }
    }
}
