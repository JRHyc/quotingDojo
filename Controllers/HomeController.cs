using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quotingDojo.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("quotes")]
        public IActionResult quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM users order by created_at desc");
            ViewBag.users = AllQuotes;
            return View();
        }
        [HttpPost]
        [Route("createQuotes")]
        public IActionResult createQuotes(string name, string quote)
        {
            string dbname = '"' + name + '"';
            string dbquote = '"' + quote + '"';
            string query = $"INSERT INTO users(name, quote, created_at) VALUES({dbname}, '{dbquote}', NOW());";
            DbConnector.Execute(query);
            return RedirectToAction("quotes");
        }
    }
}
