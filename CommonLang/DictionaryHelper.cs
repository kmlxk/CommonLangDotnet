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
    /// DictionaryHelper封装了字典类常用方法
	/// </summary>
	public class DictionaryHelper<TKey, TValue>
	{
        public DictionaryHelper()
		{
		}
		
        // 差集
		public static Dictionary<TKey, TValue> except(Dictionary<TKey, TValue> dict, List<TKey> list)
		{
			Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> kv in dict) {
				if (!list.Contains(kv.Key)) {
					ret.Add(kv.Key, kv.Value);
				}
			}
			return ret;
		}
	
		// 交集
		public static Dictionary<TKey, TValue> union(Dictionary<TKey, TValue> dict, List<TKey> list)
		{
			Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>();
			foreach (KeyValuePair<TKey, TValue> kv in dict) {
				if (list.Contains(kv.Key)) {
					ret.Add(kv.Key, kv.Value);
				}
			}
			return ret;
		}
	
	}
}
