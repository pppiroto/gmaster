using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gmaster.Models;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text;
using Dapper;
using Gmaster.Util;

namespace Gmaster.Controllers
{

    public class TableController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly GmasterDbContext _context;

        public TableController(IConfiguration configuration, GmasterDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        public IActionResult Records(string schema, string table)
        {
            var cols = from col in _context.Columns
                       where col.tabschema == schema
                       &&    col.tabname   == table
                       orderby col.colno
                       select col;

            ViewBag.Schema = schema.TrimEnd();
            ViewBag.Table = table.TrimEnd();

            return View(cols.ToList<Columns>());
        }

    }


    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly GmasterDbContext _context;

        public TablesController(IConfiguration configuration, GmasterDbContext context)
        {
            this._configuration = configuration;
            this._context = context;
        }

        [HttpGet("{schema}", Name = "Schema")]
        public object GetTablesBySchema(string schema)
        {

            var tables = from tbl in _context.Talbes
                         where tbl.tabschema == schema
                         &&   (tbl.type == "T" || tbl.type == "V")
                         orderby tbl.tabname
                         select new {
                            tabschema=tbl.tabschema,
                            tabname=tbl.tabname,
                            remarks=tbl.remarks,
                            tablename=tbl.remarks ?? tbl.tabname,
                            type=tbl.type
                         };
            return tables.ToList();
        }

        [HttpPost("{schema}/{tablename}", Name = "TableName")]
        public Dictionary<string, List<Dictionary<string, object>>> GetRecords(string schema, string tablename)
        {

            var args = new Dictionary<string, object>();
            foreach(var form in Request.Form)
            {
                var value = form.Value.ToString();
                if (!String.IsNullOrEmpty(value))
                {
                    args[form.Key] = value;
                }
            }

            var records = new List<Dictionary<string, object>>();
            var cols = from col in _context.Columns
                        where col.tabschema == schema
                        && col.tabname == tablename
                        orderby col.colno
                        select col;


            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var command = conn.CreateCommand();

            var queryBuilder = new QueryBuilder();
            var query = queryBuilder.Build(schema, tablename, cols, args, 400,0);

            Debug.WriteLine(query);
            command.CommandText = query;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var rec = new Dictionary<string, object>();
                    records.Add(rec);
                    foreach (var column in cols)
                    {
                        object value = reader[column.colno];
                        rec[column.colname] = value;
                    }
                }
            }

            var result = new Dictionary<string, List<Dictionary<string, object>>>{
                {"records", records}        
            };
            return result;
        }

        //// GET: api/Tables
        //[HttpGet]
        //public IEnumerable<Tables> GetTalbes()
        //{
        //    return _context.Talbes;
        //}
        //// GET: api/Tables/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetTables([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tables = await _context.Talbes.FindAsync(id);

        //    if (tables == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tables);
        //}

        //// PUT: api/Tables/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTables([FromRoute] string id, [FromBody] Tables tables)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tables.tabschema)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tables).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TablesExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Tables
        //[HttpPost]
        //public async Task<IActionResult> PostTables([FromBody] Tables tables)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Talbes.Add(tables);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TablesExists(tables.tabschema))
        //        {
        //            return new StatusCodeResult(StatusCodes.Status409Conflict);
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTables", new { id = tables.tabschema }, tables);
        //}

        //// DELETE: api/Tables/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTables([FromRoute] string id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var tables = await _context.Talbes.FindAsync(id);
        //    if (tables == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Talbes.Remove(tables);
        //    await _context.SaveChangesAsync();

        //    return Ok(tables);
        //}

        //private bool TablesExists(string id)
        //{
        //    return _context.Talbes.Any(e => e.tabschema == id);
        //}
    }
}