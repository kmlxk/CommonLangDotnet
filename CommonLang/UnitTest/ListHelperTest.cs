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

namespace CommonLang.UnitTest
{
	
	/// <summary>
	/// ListHelper封装了数据常用方法，主要包括toList, join
	/// </summary>
	public class ListHelperTest
	{
		[Test] public void JoinTest()
		{
			string expected = "c#,java,php,css,js,xml,ajax";
			string[] sep = expected.Split(',');
			List<string> list = ArrayHelper<string>.toList(sep);
			string actual = ListHelper<string>.join(list, ",");
			Assert.AreEqual(expected, actual);
		}
		
		[Test] public void CountTest()
		{
			string expected = "c#,java,php,css,js,xml,ajax,xml";
			string[] sep = expected.Split(',');
			List<string> list = ArrayHelper<string>.toList(sep);
			int actual = ListHelper<string>.count(list, "xml");
			Assert.AreEqual(2, actual);
		}
		
		
		[Test] public void FillTest()
		{
			List<string> list = new List<string>();
			ListHelper<string>.fill(list, "xml", 2);
			Assert.AreEqual("xml,xml", ListHelper<string>.join(list, ","));
		}

		[Test] public void findFieldTest()
		{
			List<SimpleObject> list = new List<SimpleObject>();
			list.Add(new SimpleObject(1, "c#"));
			list.Add(new SimpleObject(2, "xml"));
			list.Add(new SimpleObject(3, "js"));
			list.Add(new SimpleObject(4, "css"));
			list.Add(new SimpleObject(5, "xml"));
			list.Add(new SimpleObject(6, "java"));
			SimpleObject found;
			found = ListHelper<SimpleObject>.findField<string>(list, "name", "xml");
			Assert.AreEqual(2, found.id);
			found = ListHelper<SimpleObject>.findField<int>(list, "id", 6);
			Assert.AreEqual("java", found.name);
			found = ListHelper<SimpleObject>.findField<int>(list, "id", 7);
			Assert.IsNull(found);
			Assert.AreEqual(2, ListHelper<SimpleObject>.searchField<string>(list, "name", "xml").Count);
		}
	}
	
	class SimpleObject {
		public int id;
		public string name;
		public SimpleObject(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
	}
}
