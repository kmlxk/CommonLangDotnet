/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2013/6/22
 * Time: 16:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;

namespace CommonLang
{
	/// <summary>
	/// Description of StringHelper.
	/// </summary>
	public class StringHelper
	{
		public StringHelper()
		{
			
		}

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UpperFirst(string s)
        {
            if (s.Length > 0) {
                return s.Substring(0, 1).ToUpper() + s.Substring(1);
            }
            return s;
        }
		
		public static string repeat(string s, int count)
		{
			return repeat(s, count, "");
		}
			
		public static string repeat(string s, int count, string separator)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < count; i++) {
				sb.Append(s);
				sb.Append(separator);
			}
			string ret = sb.ToString();
			if (ret.Length > 0) {
				ret = ret.Substring(0, ret.Length - separator.Length);
			}
			return ret;
		}
	}
}
