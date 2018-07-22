using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gmaster.Controllers
{
    /// <summary>
    /// Web API 疎通確認用
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {


        /// <summary>
        ///　GET: api/Sample 
        /// </summary>
        /// <returns>["value1","value2"]</returns>        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET: api/Sample/5 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>id is 5</returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return $"id is {id}";
        }

        /// <summary>
        /// GET: api/Sample/Dict 
        /// </summary>
        /// <returns>{"Key1":"Value1","Key2":"Value1","Key3":"Value1"}</returns>
        [HttpGet("Dict", Name = "Dict")]
        public Dictionary<string, string> Dict()
        {
            var dict = new Dictionary<string, String>
            {
                { "Key1" , "Value1"},
                { "Key2" , "Value1"},
                { "Key3" , "Value1"},
            };
            return dict;
        }

        /// <summary>
        /// GET: api/Sample/List 
        /// </summary>
        /// <returns>[{"id":1,"title":"The Shawshank Redemption","year":1994},{"id":2,"title":"The Godfather","year":1972},{"id":3,"title":"The Godfather= Part II","year":1974},{"id":4,"title":"The Good, the Bad and the Ugly","year":1966},{"id":5,"title":"My Fair Lady","year":1964},{"id":6,"title":"12 Angry Men","year":1957}]</returns>
        [HttpGet("List", Name = "List")]
        public List<object> List()
        {
            var list = new List<object>()
            {
                new { id= 1, title= "The Shawshank Redemption", year= 1994 },
                new { id= 2, title= "The Godfather", year= 1972 },
                new { id= 3, title= "The Godfather= Part II", year= 1974 },
                new { id= 4, title= "The Good, the Bad and the Ugly", year= 1966 },
                new { id= 5, title= "My Fair Lady", year= 1964 },
                new { id= 6, title= "12 Angry Men", year= 1957 }
            };

            return list;
        }

        /// <summary>
        /// GET: api/Sample/Anony 
        /// </summary>
        /// <returns>{"name":"Name1","value":"Value1"}</returns>
        [HttpGet("Anony", Name = "Anony")]
        public object Anony()
        {
            var result = new {
               Name="Name1",Value="Value1"
            };
            return result;
        }


        // POST: api/Sample
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Sample/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
