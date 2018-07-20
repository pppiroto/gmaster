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
    public abstract class ConfigController : Controller
    {
        public ConfigController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }

    public abstract class DbContextController : ConfigController
    {
        private GmasterDbContext _dbContext;

        public DbContextController(IConfiguration configuration, GmasterDbContext dbContext) : base(configuration)
        {
            DbContext = dbContext;
        }

        protected DbContextController(GmasterDbContext dbContext) : this(null, dbContext)
        {
            DbContext = dbContext;
        }

        public GmasterDbContext DbContext { get => _dbContext; set => _dbContext = value; }

    }
    
    public class HomeController : DbContextController
    {
        public HomeController(IConfiguration configuration, GmasterDbContext dbContext) : base(configuration, dbContext)
        {
        }

        public IActionResult Index()
        {
            var tables = (from tbl in DbContext.Talbes
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
