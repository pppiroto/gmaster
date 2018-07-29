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

            var converter = new ValueConverter();

            foreach (var column in cols)
            {
                var key = column.colname;
                if (args.ContainsKey(key))
                {
                    var value = converter.Enclose(args[key], column);
                    builder.Where($"{key} = {value}");
                }
            }
            return templte.RawSql;
        }
    }

    public class ValueConverter
    {
        public string Enclose(object value, Columns colum)
        {
            var isEnclose = true;
            switch (colum.typename)
            {
                case "VARCHAR":
                    break;
                case "CHARACTER":
                    break;
                case "DATE":
                    break;
                case "TIME":
                    break;
                case "TIMESTAMP":
                    break;
                case "XML":
                    // TODO XML InvalidCastException
                    // using(var xmlReader = ((DB2DataReader)reader)) {
                    //     value = xmlReader.GetString(colum.colno);
                    // }
                    // if (value == null) {
                    //     value = "";
                    // }
                    //value = "TODO XML InvalidCastException";
                    break;
                case "CLOB":
                    break;
                case "INTEGER":
                    isEnclose = false;
                    break;
                case "BIGINT":
                    isEnclose = false;
                    break;
                case "DECIMAL":
                    isEnclose = false;
                    break;
                default:
                    break;
            }
            var result = (value != null) ? value.ToString() : "";

            return (isEnclose) ? $"'{result}'": result;
        }

        public string convertToString(object value, Columns colum)
        {
            switch (colum.typename)
            {
                case "VARCHAR":
                    break;
                case "CHARACTER":
                    break;
                case "DATE":
                    if (value is DateTime)
                    {
                        value = ((DateTime)value).ToString("yyyy-MM-dd");
                    }
                    break;
                case "TIME":
                    if (value is DateTime)
                    {
                        value = ((DateTime)value).ToString("HH:mm:ss.fff");
                    }
                    break;
                case "TIMESTAMP":
                    if (value is DateTime)
                    {
                        value = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    break;
                case "XML":
                    // TODO XML InvalidCastException
                    // using(var xmlReader = ((DB2DataReader)reader)) {
                    //     value = xmlReader.GetString(colum.colno);
                    // }
                    // if (value == null) {
                    //     value = "";
                    // }
                    //value = "TODO XML InvalidCastException";
                    break;
                case "CLOB":
                    break;
                case "INTEGER":
                    break;
                case "BIGINT":
                    break;
                case "DECIMAL":
                    break;
                default:
                    break;
            }

            return (value != null) ? value.ToString() : "";
        }
    }
}
