﻿/*
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
using System.Collections.Generic;
using System.IO.Compression;

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
				HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
				request.Timeout = 5000;
				request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
				request.Accept = "*/*";
				request.KeepAlive = true;
				request.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
				//request.Headers.Set("Pragma", "no-cache");
				//request.Proxy = getProxy("10.98.65.9:8080", "hhky\\lxk_8876", "lxk2011");
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
		
		public static void download(string url,string filename,string refer)
		{
			System.Net.HttpWebRequest req = System.Net.HttpWebRequest.Create(url) as System.Net.HttpWebRequest;
			req.AllowAutoRedirect = true;
			req.Referer = refer;
			req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; zh-CN; rv:1.9.2.13) Gecko/20101203 Firefox/3.6.13";
			//req.Headers.Add("Accept-Language","zh-cn,en-us;q=0.8,zh-hk;q=0.6,ja;q=0.4,zh;q=0.2");
			System.Net.HttpWebResponse res = req.GetResponse() as System.Net.HttpWebResponse;
			System.IO.Stream stream = res.GetResponseStream();
			byte[] buffer = new byte[32 * 1024];
			int bytesProcessed = 0;
			System.IO.FileStream fs = System.IO.File.Create(filename);
			int bytesRead;
			do
			{
				bytesRead = stream.Read(buffer, 0, buffer.Length);
				fs.Write(buffer, 0, bytesRead);
				bytesProcessed += bytesRead;
			}
			while (bytesRead > 0);
			fs.Flush();
			fs.Close();
			res.Close();
		}
		
		public static Dictionary<string, object> downloadLikeFirefox(string url,
		                                                  CookieContainer cookieContainer,
		                                                  Encoding encoding,
		                                                  string refer,
		                                                  WebProxy webProxy)
		{
			Dictionary<string, object> ret = new Dictionary<string, object>();
			HttpWebRequest request = null;
			string html;
			request = WebRequest.Create(url) as HttpWebRequest;
			request.ServicePoint.Expect100Continue = false;
			request.ServicePoint.UseNagleAlgorithm = false;
			request.ServicePoint.ConnectionLimit = 65500;
			request.AllowWriteStreamBuffering = false;
			if (webProxy!=null)
			{
				request.Proxy = webProxy;
			}
			else
			{
				request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
			}
			if (!String.IsNullOrEmpty(refer))
			{
				request.Referer = refer;
			}
			else
			{
				request.Referer = url;
			}
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:28.0) Gecko/20100101 Firefox/28.0";
			request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
			request.Headers.Add("Accept-Language", "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
			request.Headers.Add("Accept-Encoding", "gzip, deflate");
			request.CookieContainer = cookieContainer;
			request.Timeout = 15000;
			request.KeepAlive = true;
			
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				ret["headers"] = response.Headers;
				if (response.ContentEncoding.ToLower().Contains("gzip"))
				{
					using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
					{
						using (StreamReader reader = new StreamReader(stream, encoding))
						{
							html = reader.ReadToEnd();
						}
					}
				}
				else if (response.ContentEncoding.ToLower().Contains("deflate"))
				{
					using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
					{
						using (StreamReader reader = new StreamReader(stream, encoding))
						{
							html = reader.ReadToEnd();
						}
						
					}
				}
				else
				{
					using (Stream stream = response.GetResponseStream())
					{
						using (StreamReader reader = new StreamReader(stream, encoding))
						{
							html = reader.ReadToEnd();
						}
					}
				}
			}
			ret["cookies"] = cookieContainer;
			ret["html"] = html;
			return ret;
		}
		
	}
}
