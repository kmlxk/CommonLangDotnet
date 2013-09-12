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
	public class StringHelperTest
	{
		      
        [Test] public void StonConstructorTest()
        {
            Assert.AreEqual("arrayHelper", StringHelper.toCamelCase("array_helper"));
            Assert.AreEqual("ArrayHelper", StringHelper.toCapitalizeCamelCase("array_helper"));
        }
	}
}
