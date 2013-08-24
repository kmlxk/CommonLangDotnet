/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2013/8/24
 * 时间: 21:17
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using NUnit.Framework;

namespace CommonLang.Ston.UnitTest
{
	/// <summary>
	/// Description of StonTest.
	/// </summary>
	public class StonTest
	{
		        private const string TEST_DATA_1 = @"___TABLE:NODE
BEGIN	END	DELAY
.1	.2	5
.1	.2	5
.1	.2	5
";

        [Test] public void StonConstructorTest()
        {
            Ston ston = new Ston(TEST_DATA_1);
            Assert.AreEqual(1, ston.Tables.Count);
            Assert.AreEqual("NODE", ston.Tables[0].Name);
            Assert.AreEqual(".1", ston.Tables[0].Data[0][0]);
        }
	}
}
