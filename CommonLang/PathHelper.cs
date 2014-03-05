/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2014/3/3
 * 时间: 14:44
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;

namespace CommonLang
{
	/// <summary>
	/// Description of PathHelper.
	/// </summary>
	public class PathHelper
	{
		public PathHelper()
		{
		}
		
		public static string getFilename(string path, char ds) {
			int pos = path.LastIndexOf(ds);
			if (pos > 0) {
				return path.Substring(pos+1);
			}
			return path;
		}
		
		public static string toPath(string path) {
			return toPath(path, '\\');
		}
		
		public static string toPath(string path, char ds) {
			if (path[path.Length - 1] != ds) {
				path = path + ds;
			}
			return path;
		}
		
		public static string getFilename(string path) {
			return getFilename(path, '\\');
		}
		
		
		public static string getDirectory(string path, char ds) {
			int pos = path.LastIndexOf(ds);
			if (pos > 0) {
				return path.Substring(0, pos + 1);
			}
			return string.Empty;
		}
		
		public static string getDirectory(string path) {
			return getDirectory(path, '\\');
		}
		
	}
}
