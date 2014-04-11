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
	public class DbAdapterTest
	{
		[Test] public void configTest()
		{
			DbHelper da = DbHelper.getInstance();
			Assert.AreEqual("test", da.getTablename("test"));
			
			DbAdapterConfig config = new DbAdapterConfig();
			config.TableName = "da_{0}";
			config.PrimaryKey = "{0}_id";
			
			DbHelper da1 = new DbHelper(config);
			Assert.AreEqual("da_test", da1.getTablename("test"));
		}
		
		[Test] public void getInsertTest()
		{
			
			DbAdapterConfig config = new DbAdapterConfig();
			config.TableName = "da_{0}";
			config.PrimaryKey = "{0}_id";
			
			DbHelper da = new DbHelper(config);
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict["title"] = "是否可行";
			dict["question"] = "是否";
			dict["answer"] = "是的";
			dict["askname"] = "小y";
			dict["createtime"] = new DbLiteral("NOW");
			string sql = da.getInsert("zhidao", dict);
			Console.Write(sql);
			Assert.AreEqual("INSERT INTO da_zhidao (title,question,answer,askname,createtime) VALUES('是否可行','是否','是的','小y',NOW)", sql);
		}
		[Test] public void getDeleteTest()
		{
			
		}
		
	}
}
