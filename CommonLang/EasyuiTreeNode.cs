using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLang
{
    public class EasyuiTreeNode<Tpk, TAttr>
    {
        public Tpk id;
        public string text;
        public string state;
        public bool fchecked; // 似乎checked在c#是保留关键字，需要替换
        public TAttr attributes;
        public List<EasyuiTreeNode<Tpk, TAttr>> children;

        public static string convertFields(string json)
        {
            return json.Replace("fchecked", "checked");
        }
    }

    
}
