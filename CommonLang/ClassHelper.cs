/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2013/9/3
 * Time: 15:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;
using System.Collections.Generic;

namespace CommonLang
{
	/// <summary>
	/// Description of ClassHelper.
	/// </summary>
	public class ClassHelper
	{

        public static Dictionary<string, T1> toDictionary<T1>(Object obj) where T1 : new()
        {
            Dictionary<string, T1> dict = new Dictionary<string, T1>();
            Type type = obj.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                dict.Add(prop.Name, (T1)Convert.ChangeType(prop.GetValue(obj, null), typeof(T1)));
            }
            return dict;
        }

		public static T1 fromDictionary<T1>(IDictionary<string, object> dict) where T1 : new()
		{
			T1 obj = new T1();
			Type type = typeof(T1);
			foreach (KeyValuePair<string, object> kv in dict) {
				PropertyInfo prop = type.GetProperty(kv.Key);
				if (prop == null)
					continue;
				Type needType = prop.PropertyType;                    
                prop.SetValue(obj, Convert.ChangeType(kv.Value, needType), null);
			}
			return obj;
		}
		
		public static T1 fromArray<T1>(object[] ar) where T1 : new()
		{
			PropertyInfo[] props = typeof(T1).GetProperties();
            List<T1> ret = new List<T1>();
            T1 obj = new T1();
            for (int i = 0; i < ar.Length; i++)
            {
                if (i >= props.Length)
                {
                    continue;
                }
                Type needType = props[i].PropertyType;                    
                props[i].SetValue(obj, Convert.ChangeType(ar[i], needType), null);
            }
            return obj;
		}

        /// <summary>
        /// 将数据库查询返回的多个字段，逐一填充到指定的类字段中。
        /// 建议字段定义为public string fieldname {get;set;};
        /// 注：未测试类型转换
        /// </summary>
        /// <typeparam name="T1">待填充的类型</typeparam>
        /// <param name="list">NHibernate返回的IList，通常为ArrayList</param>
        /// <returns></returns>
        public static List<T1> fromListOfArray<T1>(System.Collections.IList list) where T1 : new()
        {
        	PropertyInfo[] props = typeof(T1).GetProperties();
            List<T1> ret = new List<T1>();
            foreach (object[] ar in list)
            {
                T1 obj = new T1();
                for (int i = 0; i < ar.Length; i++)
                {
                    if (i >= props.Length)
                    {
                        continue;
                    }
                    Type needType = props[i].PropertyType;                    
                    props[i].SetValue(obj, Convert.ChangeType(ar[i], needType), null);
                }
                ret.Add(obj);
            }

            return ret;
        }
        
        
		public static string serializeFields<T1>(T1 obj) where T1 : class
		{
			List<string> ret = new List<string>();
			FieldInfo[] fields = typeof(T1).GetFields();
            foreach (FieldInfo field in fields) {
				ret.Add(field.Name + "=" + field.GetValue(obj));
            }
			return CommonLang.ListHelper<string>.join(ret, "; ");
		}
		
		public static string serialize<T1>(T1 obj) where T1 : class
		{
			return serializeFields(obj) + " " + serializeProperties(obj);
		}
        
		public static string serializeProperties<T1>(T1 obj) where T1 : class
		{
			List<string> ret = new List<string>();
			PropertyInfo[] props = typeof(T1).GetProperties();
            foreach (PropertyInfo prop in props) {
				ret.Add(prop.Name + "=" + prop.GetValue(obj, null));
            }
			return CommonLang.ListHelper<string>.join(ret, "; ");
		}

    }
}

