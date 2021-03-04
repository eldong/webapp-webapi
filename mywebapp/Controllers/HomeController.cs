using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace mywebapp.Controllers
{
    public class HomeController : Controller
    {
        HttpClient client;
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            client = new HttpClient();
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Quotes()
        {

           var host = _config["ApiHost"];
            //var response = await client.GetStringAsync($"http://{host}/api/quotes");
            var response = await client.GetStringAsync("http://demowebapi:9000/api/quotes");
            //var sessions = JsonConvert.DeserializeObject&amp;lt;List&amp;lt;Session&amp;gt;&amp;gt;(response);
            ViewData["Message"] = response; //"Sessions";
            
            return View();
         }
    }
}
