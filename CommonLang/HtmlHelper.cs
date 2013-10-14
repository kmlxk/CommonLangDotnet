using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
