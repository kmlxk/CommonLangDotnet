﻿/*
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

        public static TValue[][] toArray2d(List<Dictionary<TKey, TValue>> list)
        {
            List<TValue[]> rows = new List<TValue[]>();
            List<TValue> listHeaders = new List<TValue>();
            foreach (Dictionary<TKey, TValue> obj in list)
            {
                if (listHeaders.Count == 0)
                {
                    foreach (TKey key in obj.Keys)
                    {
                        listHeaders.Add((TValue)Convert.ChangeType(key, typeof(TValue)));
                    }
                    rows.Add(listHeaders.ToArray());
                }
                TValue[] row = new TValue[listHeaders.Count];
                int col = 0;
                foreach (TValue val in obj.Values)
                {
                    row[col] = val;
                }
                rows.Add(row);
            }
            return rows.ToArray();
        }

        // 字符串化
        public static Dictionary<string, string> toString(Dictionary<TKey, TValue> dict)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach (KeyValuePair<TKey, TValue> kv in dict)
            {
                ret.Add(kv.Key.ToString(), kv.Value.ToString());
            }
            return ret;
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


        public static Dictionary<T1, T2> changeType<T1, T2>(Dictionary<TKey, TValue> dict)
        {
            Dictionary<T1, T2> ret = new Dictionary<T1, T2>();
            foreach (KeyValuePair<TKey, TValue> kv in dict)
            {
                ret.Add((T1)Convert.ChangeType(kv.Key, typeof(T1)), 
                    (T2)Convert.ChangeType(kv.Value, typeof(T2))
                    );
            }
            return ret;
        }
    }
}