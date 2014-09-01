using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLang
{
	public class HtmlHelper
	{

		public static string toTable(string[][] ar)
		{
			return toTable(ar, null);
		}

		public static string toTable(string[][] ar, string[] headers)
		{
			int i, j;
			List<string> table = new List<string>();
			table.Add("<table tableProp>");
			List<string> header = new List<string>();
			header.Add("<thead><tr>");
			for (i = 0; i < headers.Length; i++)
			{
				header.Add("<th>");
				header.Add(headers[i]);
				header.Add("</th>");
			}
			header.Add("</tr><tfoot>\n");
			table.Add(CommonLang.ListHelper<string>.join(header));

			for (i = 0; i < ar.Length; i++)
			{
				List<string> row = new List<string>();
				row.Add("<tr trProp class='" + (i%2==0?"even":"odd") + "'>");
				for (j = 0; j < ar[i].Length; j++)
				{
					row.Add("<td>");
					row.Add(ar[i][j]);
					row.Add("</td>");
				}
				row.Add("</tr>\n");
				table.Add(CommonLang.ListHelper<string>.join(row));
			}
			table.Add("</table>");
			return CommonLang.ListHelper<string>.join(table);
		}
		
		
		public static string stripHTML(string html) //去除HTML标记
		{
			html = Regex.Replace(html, @"<script[^>]*?>.*?<\script>", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);

			html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
			html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);

			html.Replace("<", "");
			html.Replace(">", "");
			html.Replace("\r\n", "");
			return html;
		}
	}
}
