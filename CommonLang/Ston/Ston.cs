using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLang.Ston
{
    public class Ston
    {
        protected List<Table> _tables = new List<Table>();
        // 表名称与index对应表
        private Dictionary<string, int> _tableIndexMap = new Dictionary<string, int>();
        private Dictionary<string, Table> _tableMap = new Dictionary<string, Table>();
        
        public Table getTable(string tableName)
        {
        	return _tableMap[tableName];
        }

        public List<Table> Tables
        {
            get { return _tables; }
            set { _tables = value; }
        }
        
        public Ston(string ston)
        {
            unserialize(ston);
        }

        public void unserialize(string ston)
        {
            int pos1 = -1;
            string[] lines = ston.Replace("\r", "").Split('\n');
            List<string> list = CommonLang.ArrayHelper<string>.toList(lines);
            List<string> sublist = null;
            Table table;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (i == lines.Length - 1 ||
                    line.StartsWith("___TABLE"))
                {
                    if (pos1 != -1)
                    {
                        // another table
                        string tableName = lines[pos1].Substring("___TABLE:".Length);
                        sublist = list.GetRange(pos1 + 1, i - pos1 - 1);
                        table = new Table(CommonLang.ArrayHelper<string>.join(sublist.ToArray(), "\n"), true);
                        tableName = tableName.Trim(new char[]{'\t'});
                        table.Name = tableName;
                        _tableIndexMap[tableName] = _tables.Count;
                        _tableMap[tableName] = table;
                        _tables.Add(table);
                    }
                    pos1 = i;
                }
            }
        }
    }
}
