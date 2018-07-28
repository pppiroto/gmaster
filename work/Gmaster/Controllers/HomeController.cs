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
        private readonly IConfiguration _configuration;
        private readonly GmasterDbContext _context;

        public HomeController(IConfiguration configuration, GmasterDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        public IActionResult Index()
        {
            ViewBag.DefaultSchema = _configuration.GetSection("DbSettings")["DefaultSchema"];
            return View();
        }

        public IActionResult UiTrial()
        {
            return View();
        }
    }
}
