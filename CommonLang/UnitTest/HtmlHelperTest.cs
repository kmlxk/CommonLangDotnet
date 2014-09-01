/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2014/9/1
 * 时间: 21:08
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using NUnit.Framework;

namespace CommonLang.UnitTest
{
	[TestFixture]
	public class HtmlHelperTest
	{
		[Test]
		public void TestMethod()
		{
			string html = "<SPAN style='COLOR: #31849b'>看电视还在纠结节目档期吗？输入“(芒果台)有什么电视节目”便可轻松获得节目单~</SPAN>";
			string text = CommonLang.HtmlHelper.stripHTML(html);
			Console.WriteLine("text:" + text);
		}
	}
}
