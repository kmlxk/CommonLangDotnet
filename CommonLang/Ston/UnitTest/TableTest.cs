/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2013/8/24
 * 时间: 21:18
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CommonLang.Ston.UnitTest
{
	/// <summary>
	/// Description of TableTest.
	/// </summary>
	public class TableTest
	{
		private const string TEST_DATA_1 = @"BEGIN	END	DELAY
.1	.2	5
.1	.2	5
.1	.2	5";    

		
		[Test] public void TableBasicTest()
        {
            Table table = new Table(TEST_DATA_1, true);
            // 测试读取数据
            Assert.AreEqual(3, table.Headers.Count);
            Assert.AreEqual("BEGIN", table.Headers[0]);
            Assert.AreEqual("END", table.Headers[1]);
            Assert.AreEqual("DELAY", table.Headers[2]);
            Dictionary<string,string> dict = table.getDictionary(0);
            Assert.AreEqual(".1", dict["BEGIN"]);
            // 测试更新数据
            table.Data[0][0] = ".100";
            table.set("END", 0, ".200");
            Dictionary<string, string> dict2 = table.getDictionary(0);
            Assert.AreEqual(".100", dict2["BEGIN"]);
            Assert.AreEqual(".200", dict2["END"]);
            string actural = table.serialize();
            Assert.AreNotEqual(TEST_DATA_1.Replace("\r", ""), actural);
        }
		
		[Test]
        public void escapeTest()
        {
            string ston = string.Empty;
            bool hasHeader = false;
            Table target = new Table(ston, hasHeader);
            string actual;
            actual = target.escape("\n\r\t");
            Assert.AreEqual("\\nl\\rt\\tab", actual);
        }

        [Test]
        public void serializeTest()
        {
            Table table = new Table(TEST_DATA_1, true);
            string actural = table.serialize();
            Assert.AreEqual(TEST_DATA_1.Replace("\r", ""), actural);
        }
        
        [Test]
        public void toArrayTest()
        {
            Table table = new Table(TEST_DATA_1, true);
            string[][] actural = table.toArray();
            Assert.AreEqual(3, actural.Length);
            Assert.AreEqual(3, actural[0].Length);
            Assert.AreEqual(".1", actural[0][0]);
        }
        
        [Test]
        public void getColTest()
        {
            Table table = new Table(TEST_DATA_1, true);
            string[] actural = table.getCol(0);
            Assert.AreEqual(3, actural.Length);
            Assert.AreEqual(".1", actural[0]);
        }
        
		[Test]
        public void unescapeTest()
        {
            string ston = string.Empty;
            bool hasHeader = false;
            Table target = new Table(ston, hasHeader);
            string actual;
            actual = target.unescape("\\nl\\rt\\tab");
            Assert.AreEqual("\n\r\t", actual);
        }


	}
}
