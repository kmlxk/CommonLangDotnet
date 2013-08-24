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

namespace CommonLang
{
	/// <summary>
	/// ArrayHelper封装了数据常用方法，主要包括toList, join
	/// </summary>
	public class ArrayHelper<T>
	{
		public ArrayHelper()
		{
		}
		
		public static string join(T[] ar)
		{
			return join(ArrayHelper<T>.toList(ar));
		}
		
		public static string join(T[] ar, string separator)
		{
			return join(ArrayHelper<T>.toList(ar), separator);
		}

        public static string join(List<T> list)
        {
            return ListHelper<T>.join(list);
        }

        public static string join(List<T> list, string separator)
        {
            return ListHelper<T>.join(list, separator);
        }
		
		/// <summary>
		/// 数组转换为List
		/// </summary>
		/// <param name="array">基本数组</param>
		/// <returns>List</returns>
		public static List<T> toList(T[] array)
		{
			// 预申请空间可以提升效率
			int capacity = array.Length;
			if (capacity <= 0) {
				capacity = 10;
			}
			List<T> list = new List<T>(capacity);
			for (int i = 0; i < array.Length; i++) {
				list.Add(array[i]);
			}
			return list;
		}
		
	}
}
