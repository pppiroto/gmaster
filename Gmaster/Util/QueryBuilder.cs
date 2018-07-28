using Dapper;
using Gmaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmaster.Util
{
    public class QueryBuilder
    {

        /// <summary>
        /// 
        /// </summary>
        /// <see cref="https://liangwu.wordpress.com/2012/08/20/dapper-net-tutorial-ii/"/>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="cols"></param>
        /// <param name="args"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public string Build(string schema, string table, IEnumerable<Columns> cols, Dictionary<string, object> args, int? limit = null, int? offset = 0)
        {
            var builder = new SqlBuilder();
            var templte = builder.AddTemplate($"SELECT * /**select**/ FROM {schema}.{table} /**where**/" 
                + ((limit !=null && offset != null)?$" LIMIT {limit} OFFSET {offset}":""));

            foreach (var column in cols)
            {
                var key = column.colname;
                if (args.ContainsKey(key))
                {
                    var value = args[key];
                    builder.Where($"{key} = '{value}'");
                }
            }
            return templte.RawSql;
        }

    }
}
