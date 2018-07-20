using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gmaster.Models
{
    [Table("tables", Schema = "syscat")]
    public class Tables
    {
        [DisplayName("スキーマ")]
        public string tabschema { get; set; }
        [DisplayName("表名(物理)")]
        public string tabname { get; set; }
        [DisplayName("所有者")]
        public string owner { get; set; }
        [DisplayName("所有者タイプ")]
        public string ownertype { get; set; }
        [DisplayName("表タイプ")]
        public string type { get; set; }

        [DisplayName("作成日時")]
        [Column("create_time")]
        public DateTime createTime { get; set; }
        [DisplayName("変更日時")]
        [Column("alter_time")]
        public DateTime alterTime { get; set; }
        [DisplayName("列数")]
        public short colcount { get; set; }
        [DisplayName("表名")]
        public string remarks { get; set; }

    }

    [Table("columns", Schema = "syscat")]
    public class Columns
    {
        public string tabschema { get; set; }
        public string tabname { get; set; }
        public string colname { get; set; }
        public short colno { get; set; }
        public string typeschema { get; set; }
        public string typename { get; set; }
        public int? length { get; set; }
        public short? scale { get; set; }
        public string nulls { get; set; }
        public short? keyseq { get; set; }
        public string remarks { get; set; }
    }

    public class AnonymousRecord : Dictionary<String, object>
    {
        public AnonymousKey Key { get; }
        public AnonymousRecord()
        {
            Key = new AnonymousKey();
        }
    }

    public class AnonymousKey : Dictionary<String, object>
    {
    }

}


//// データ取得サンプル(EF)
//var sql = $"select * from syscat.tables fetch first 5 rows only";
//
//// LINQ Method
//// by SQL
//// var tables = DbContext.Tables.FromSql(sql);  
//
//// fetch all
//// var tables = DbContext.Set<Tables>(); 
//
//// LINQ query
//var tables = from table in DbContext.Tables
//            where table.tabschema == "SALESMS"
//            select table;
//
//foreach(var table in tables) {
//    Debug.WriteLine($"TABLE : {table.tabschema}.{table.tabname} {table.owner} {table.ownertype} {table.type}  {table.createTime} {table.alterTime} {table.colcount}");     
//
//    var colmns = from column in DbContext.Columns
//                where column.tabname == table.tabname
//                orderby column.colno ascending
//                select column
//                ;
//    foreach(var colmn in colmns) {
//        Debug.WriteLine($"    {colmn.colno} {colmn.colname} {colmn.keyseq} {colmn.typename} {colmn.length} {colmn.scale} {colmn.remarks}");
//    }
//}
//// データ取得サンプル(ADO)
//using(var conn = DbContext.Database.GetDbConnection()) {
//    if (conn.e != ConnectionState.Open) {
//        conn.Open();
//    }
//    var command = conn.CreateCommand();
//    command.CommandText = sql;
//    using(var reader = command.ExecuteReader()) {
//        while(reader.Read()) {
//            Debug.WriteLine($"{reader["tabschema"]}.{reader["tabname"]}");     
//        }
//    }
//}

