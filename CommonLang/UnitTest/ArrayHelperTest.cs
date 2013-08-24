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
	/// ArrayHelper封装了数据常用方法，主要包括toList, join
	/// </summary>
	public class ArrayHelperTest
	{
		[Test] public void JoinTest()
		{
			string expected = "c#,java,php,css,js,xml,ajax";
			string[] sep = expected.Split(',');
			string actual = ArrayHelper<string>.join(sep, ",");
			Assert.AreEqual(expected, actual);
		}
		
		[Test] public void ToListTest()
		{
			string expected = "c#,java,php,css,js,xml,ajax";
			string[] sep = expected.Split(',');
			List<string> list = ArrayHelper<string>.toList(sep);
			Assert.AreEqual(sep.Length, list.Count);
			Assert.AreEqual(sep[0], list[0]);
		}
		
	}
}
