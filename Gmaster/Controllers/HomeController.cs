using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gmaster.Models;
using Microsoft.Extensions.Configuration;

namespace Gmaster.Controllers
{
    
    public class HomeController : Controller
    {
        private GmasterDbContext _context;
        public HomeController(GmasterDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var tables = (from tbl in _context.Talbes
                          select tbl).First();

            Debug.WriteLine(tables.tabname);

            return View();
        }

        public IActionResult UiTrial()
        {
            return View();
        }
    }
}
