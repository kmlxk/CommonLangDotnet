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

namespace CommonLang.UnitTest
{
	class User
	{
		public string username {get; set;}
		public string password {get; set;}
		public int type {get; set;}
		public string email {get; set;}
		
			
	}
	/// <summary>
	/// Description of TableTest.
	/// </summary>
	public class ClassHelperTest
	{
		[Test] public void fromListOfArrayTest()
        {
            object[] ar = new object[] {"admin", "123456", 1, "k@1.com"};
			System.Collections.IList list = 
				new System.Collections.ArrayList();
			list.Add(ar);
			List<User> users = ClassHelper.fromListOfArray<User>(list);
			Assert.AreEqual(1, users.Count);
			Assert.AreEqual("admin", users[0].username);
			Assert.AreEqual(1, users[0].type);
        }
		
		[Test] public void fromDictionaryTest()
        {
			Dictionary<string, object> dict = new Dictionary<string, object>();
			dict["username"] = "admin";
			dict["password"] = "123456";
			dict["type"] = 1;
			dict["email"] = "kx@163.com";
            User user = ClassHelper.fromDictionary<User>(dict);
			Assert.AreEqual("admin", user.username);
			Assert.AreEqual(1, user.type);
        }
		
		[Test] public void fromArrayTest()
        {
            object[] ar = new object[] {"admin", "123456", 1, "k@1.com"};
            User user = ClassHelper.fromArray<User>(ar);
			Assert.AreEqual("admin", user.username);
			Assert.AreEqual(1, user.type);
        }

        [Test]
        public void serializeTest()
        {
        	object[] ar = new object[] {"admin", "123456", 1, "k@1.com"};
			System.Collections.IList list = 
				new System.Collections.ArrayList();
			list.Add(ar);
			List<User> users = ClassHelper.fromListOfArray<User>(list);
            string str = ClassHelper.serialize<User>(users[0]);
            Assert.True(str.IndexOf("username") > 0);
            Assert.True(str.IndexOf("admin") > 0);
        }
        
	}
}
