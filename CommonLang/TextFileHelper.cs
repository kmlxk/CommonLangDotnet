/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2013/8/2
 * Time: 8:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;

namespace CommonLang
{
	/// <summary>
	/// TextFileHelper用于读取文本文件
	/// </summary>
	public class TextFileHelper
	{

		public static string readAll(string filename, Encoding enc)
		{
			FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
			string line;
			StringBuilder sb = new StringBuilder();
			StreamReader sr;
			if (enc != null) {
				sr = new StreamReader(file, enc);
			} else {
				sr = new StreamReader(file);
			}
			line = sr.ReadLine();
			while(line != null)
			{
				sb.Append(line);
				sb.Append(Environment.NewLine);
				line = sr.ReadLine();
			}
			sr.Close();
			return sb.ToString();
		}
		
		public static void writeAll(string filename,string content, Encoding enc)
		{
			FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamWriter sw;
			if (enc != null) {
				sw = new StreamWriter(file, enc);
			} else {
				sw = new StreamWriter(file);
			}
			sw.Write(content);
			sw.Close();
		}

		
	}
}
