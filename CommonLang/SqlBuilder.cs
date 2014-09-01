using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CommonLang.orm
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
	
	public class SqlBuilderConfig
	{
		public string TableName{get;set;}
		public string PrimaryKey{get;set;}
	}
	
	public class SqlLiteral
	{
		protected string _sql;
		public SqlLiteral(string sql)
		{
			_sql = sql;
		}
		
		public override string ToString()
		{
			return _sql;
		}
	}
	
	public class SqlBuilder
	{
		public static SqlBuilder instance = new SqlBuilder();
		protected SqlBuilderConfig _config;
		
		public SqlBuilder()
		{
			_config = new SqlBuilderConfig();
			_config.TableName = "{0}";
			_config.PrimaryKey = "{0}_id";
		}
		
		public SqlBuilder(SqlBuilderConfig config)
		{
			_config = config;
		}
		public static SqlBuilder getInstance()
		{
			return instance;
		}
		
		
		public string getTablename(string tablename)
		{
			return string.Format(_config.TableName, tablename);
		}
		public string getInsert(string tablename, Dictionary<string, object> dict)
		{
			StringBuilder sb = new StringBuilder();
			List<string> keys = dict.Keys.ToList();
			List<string> values = new List<string>(keys.Count);
			foreach(string key in keys)
			{
				if (dict[key] == null) {
					values.Add("''");
				}else if (dict[key].GetType().Equals(typeof(SqlLiteral))) {
					
					values.Add(dict[key].ToString());
				} else
				{
					values.Add("'"+dict[key].ToString()+"'");
				}
			}
			string sKeys = ListHelper<string>.join(keys, ",");
			string sValues = ListHelper<string>.join(values, ",");
			string sql = "INSERT INTO {0} ({1}) VALUES({2})";
			return string.Format(sql, getTablename(tablename), sKeys, sValues);
		}
		public string getDelete(string tablename, string where)
		{
			StringBuilder sb = new StringBuilder();
			string sql = "DELETE FROM {0} WHERE {1}";
			return string.Format(sql, getTablename(tablename), where);
		}
		public string getSelect(string tablename, string fields, string where, string suffix)
		{
			if (fields == null || fields.Length == 0)
				fields = "*";
			if (where == null || where.Length == 0)
				where = "";
			if (suffix == null || suffix.Length == 0)
				suffix = "";
			StringBuilder sb = new StringBuilder();
			string sql = "SELECT {0} FROM {1} {2} {3}";
			return string.Format(sql, new object[]{fields, getTablename(tablename), where, suffix});
		}
	}
}
