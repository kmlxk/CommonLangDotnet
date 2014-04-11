using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLang.ORM
{

    public class SqlSelectBuilder
    {
        public string _cond = "";
        public string _order = "";

        public SqlSelectBuilder find(string where) {
            _cond = where;
            return this;
        }

        public SqlSelectBuilder find(Dictionary<string, object> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, object> kv in dict)
            {
                sb.Append(kv.Key+"='"+kv.Value+"' ");
            }
            _cond = sb.ToString();
            return this;
        }

        public SqlSelectBuilder order(string order)
        {
            _order = order;
            return this;
        }


        public override string ToString()
        {   
            return "";
        }
    }
    public class DbAdapter
    {
        public static DbAdapter instance = new DbAdapter();

        public DbAdapter getInstance()
        {
            return instance;
        }

        public string getInsert(string tablename, List<string> keys, List<string> values)
        {
            StringBuilder sb = new StringBuilder();
            string sKeys = ListHelper<string>.join(keys, ",");
            string sValues = "'" + ListHelper<string>.join(values, "','") + "'";
            string sql = "INSERT INTO {0} ({1}) VALUES({1})";
            return string.Format(sql, tablename, sKeys, sValues);

        }

        public string getDelete(string tablename, string where)
        {
            StringBuilder sb = new StringBuilder();
            string sql = "DELETE FROM {0} WHERE {1}";
            return string.Format(sql, tablename, where);

        }

        public string getSelect(string tablename, string where)
        {
            StringBuilder sb = new StringBuilder();
            string sql = "SELECT * FROM {0} WHERE {1}";
            return string.Format(sql, tablename, where);
        } 
    }
}
