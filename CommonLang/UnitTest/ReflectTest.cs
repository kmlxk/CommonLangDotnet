/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2013/8/24
 * 时间: 21:17
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Reflection;
using NUnit.Framework;

namespace CommonLang.Ston.UnitTest
{

    /// <summary>
    /// Description of StonTest.
    /// </summary>
    public class ReflectTest
    {

        public class CTester
        {
        	public int Id{set;get;}
            public CTester()
            {
            }
            public void test1()
            {
            }
            
        }

        [Test]
        public void ReflectEffiencyTest()
        {
        	// init
            DateTime now = DateTime.Now;
            CTester obj = new CTester();
            Type type = typeof(CTester);
            TimeSpan elapse;
            
        	// direct method invoke
        	now = DateTime.Now;
            for (int i = 0; i < 1000000; i++)
            {
            	obj.test1();
            }
            elapse = DateTime.Now - now;
            Console.WriteLine("direct method invoke: " + elapse.TotalMilliseconds);
            
            // reflect method invoke
            now = DateTime.Now;
            MethodInfo method = type.GetMethod("test1");
            for (int i = 0; i < 1000000; i++)
            {    	
            	method.Invoke(obj, BindingFlags.InvokeMethod, null,null,null);
            }
            elapse = DateTime.Now - now;
            Console.WriteLine("reflect method invoke: " + elapse.TotalMilliseconds);
            
            // fast reflect method invoke
            now = DateTime.Now;
            MethodInfo methodInfo = type.GetMethod("test1");
            FastMethodInvoker.FastInvokeHandler fastInvoker = FastMethodInvoker.FastInvoker.GetMethodInvoker(methodInfo);
            for (int i = 0; i < 1000000; i++)
            {    	            	
            	fastInvoker(obj, null);
            }
            elapse = DateTime.Now - now;
            Console.WriteLine("fast reflect method invoke: " + elapse.TotalMilliseconds);
            
        	// direct property access
            for (int i = 0; i < 1000000; i++)
            {
            	int id = obj.Id;
            }
            elapse = DateTime.Now - now;
            Console.WriteLine("direct property access: " + elapse.TotalMilliseconds);
            
            // reflect method invoke
            now = DateTime.Now;
            PropertyInfo prop = type.GetProperty("Id");
            for (int i = 0; i < 1000000; i++)
            {    	
            	int id = (int)prop.GetValue(obj, null);
            }
            elapse = DateTime.Now - now;
            Console.WriteLine("reflect property access: " + elapse.TotalMilliseconds);
        }
    }
}
