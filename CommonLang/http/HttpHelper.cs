/*
 * 由SharpDevelop创建。
 * 用户： Administrator
 * 日期: 2014/3/3
 * 时间: 10:44
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */
using System;
using System.Text;
using System.Net;
using System.IO;

namespace CommonLang.http
{
	/// <summary>
	/// Description of HttpHelper.
	/// </summary>
	public class HttpHelper
	{
		public HttpHelper()
		{
		}
		
		public static WebProxy getProxy(string url, string username, string password)
		{
			if (url.Contains(":"))
			{
				string[] plist = url.Split(':');
				WebProxy proxy = new WebProxy(plist[0].Trim(), Convert.ToInt32(plist[1].Trim()));
				proxy.Credentials = new NetworkCredential(username, password);
				return proxy;
			}
			else
			{
				WebProxy proxy = new WebProxy(url, false);
				proxy.Credentials = new NetworkCredential(username, password);
				return  proxy;
			}
		}
		
		public static string getHtml(string url, string encoding)
		{
			string str = string.Empty;
			try
			{
				WebRequest request = WebRequest.Create(url);
				request.Timeout = 30000;
				//request.Headers.Set("Pragma", "no-cache");
				request.Proxy = getProxy("10.98.65.9:8080", "hhky\\lxk_8876", "lxk2011");
				WebResponse response = request.GetResponse();
				Stream streamReceive = response.GetResponseStream();
				Encoding enc = Encoding.GetEncoding(encoding);
				StreamReader streamReader = new StreamReader(streamReceive, enc);
				str = streamReader.ReadToEnd();
				streamReader.Close();
			}
			catch (Exception ex)
			{ }
			return str;
		}
	}
}
