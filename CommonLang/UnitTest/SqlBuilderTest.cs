/*
 * Created by SharpDevelop.
 * Author: kmlxk
 * Date: 2013/6/18
 * Time: 15:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CommonLang.orm;

namespace CommonLang.UnitTest
{
	public class SqlBuilderTest
	{
		[Test] public void configTest()
		{
			SqlBuilder da = SqlBuilder.getInstance();
			Assert.AreEqual("test", da.getTablename("test"));
			
			SqlBuilderConfig config = new SqlBuilderConfig();
			config.TableName = "da_{0}";
			config.PrimaryKey = "{0}_id";
			
			SqlBuilder da1 = new SqlBuilder(config);
			Assert.AreEqual("da_test", da1.getTablename("test"));
		}
		
		[Test] public void getInsertTest()
		{
			
			SqlBuilderConfig config = new SqlBuilderConfig();
			config.TableName = "da_{0}";
			config.PrimaryKey = "{0}_id";
			
			SqlBuilder da = new SqlBuilder(config);
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict["title"] = "是否可行";
			dict["question"] = "是否";
			dict["answer"] = "是的";
			dict["askname"] = "小y";
			dict["createtime"] = new SqlLiteral("NOW");
			string sql = da.getInsert("zhidao", dict);
			Console.Write(sql);
			Assert.AreEqual("INSERT INTO da_zhidao (title,question,answer,askname,createtime) VALUES('是否可行','是否','是的','小y',NOW)", sql);
		}
		[Test] public void getSelectTest()
		{
			
			SqlBuilderConfig config = new SqlBuilderConfig();
			config.TableName = "da_{0}";
			config.PrimaryKey = "{0}_id";
			
			SqlBuilder da = new SqlBuilder(config);
			string actual;
			string expected;
			
			// 1
			actual= da.getSelect("item", "", "", "limit 1,2");
			expected = "SELECT * FROM da_item  limit 1,2";
			Assert.AreEqual(expected, actual);
			
			// 2
			actual= da.getSelect("item", "id, name, title", "a=1", "order by desc");
			expected = "SELECT id, name, title FROM da_item a=1 order by desc";
			Console.Write(actual);
			Assert.AreEqual(expected, actual);
		}
		
	}
}
