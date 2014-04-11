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
        public static string upperFirst(string s)
        {
            if (s.Length > 0)
            {
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
            for (int i = 0; i < count; i++)
            {
                sb.Append(s);
                sb.Append(separator);
            }
            string ret = sb.ToString();
            if (ret.Length > 0)
            {
                ret = ret.Substring(0, ret.Length - separator.Length);
            }
            return ret;
        }

        public static string toCapitalizeCamelCase(string s)
        {
            if (s == null)
            {
                return null;
            }
            s = toCamelCase(s);
            return s.Substring(0, 1).ToUpper() + s.Substring(1);
        }

        public static string toCamelCase(string s)
        {
            return toCamelCase(s, '_');
        }

        public static string toCamelCase(string s, char SEPARATOR)
        {
            if (s == null)
            {
                return null;
            }
            s = s.ToLower();
            StringBuilder sb = new StringBuilder(s.Length);
            bool upperCase = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == SEPARATOR)
                {
                    upperCase = true;
                }
                else if (upperCase)
                {
                    sb.Append(Char.ToUpper(c));
                    upperCase = false;
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }


        public static int toInt(object parentId)
        {
            // 如果是空白字符串或者"0"都返回0
            try
            {
                if ((parentId.GetType()).Equals(typeof(String)))
                {
                    return Convert.ToInt32("0" + parentId);
                }
                return Convert.ToInt32(parentId);
            }
            catch (Exception ex)
            {
                // 如果不能转换为数字，返回-1
                return -1;
            }

        }

        public static string fromCamelCase(string s)
        {
            return fromCamelCase(s, '_');
        }

        public static string fromCamelCase(string s, char SEPARATOR)
        {
            if (s == null || s.Length == 0)
            {
                return null;
            }
            StringBuilder sb = new StringBuilder(s.Length);
            sb.Append(s[0]); 
            for (int i = 1; i < s.Length; i++)
            {
                char c = s[i];
                char c0 = s[i-1];
                if ((c >= 65 && c <= 90) && // cur char is uppercase
                    (c0 >= 97 && c0 <= 122))// last char is lowercase
                {
                    sb.Append(SEPARATOR); 
                    sb.Append(c);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString().ToLower();
        }
    }
}
