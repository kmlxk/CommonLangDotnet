using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLang.Ston
{
    public class Table
    {
        // 列头与index对应表
        private Dictionary<string, int> _columnIndexMap = new Dictionary<string, int>();
        // 按行排序的数据
        private List<List<string>> _data = new List<List<string>>();
        // 如果有列头才会填充_columnIndexMap
        protected bool _hasHeader = true;
        // 列头
        private List<string> _headers;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Dictionary<string, int> ColumnIndexMap
        {
            get { return _columnIndexMap; }
            set { _columnIndexMap = value; }
        }
        public List<List<string>> Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public List<string> Headers
        {
            get { return _headers; }
            set { _headers = value; }
        }

        public bool HasHeader
        {
            get { return _hasHeader; }
            set { _hasHeader = value; }
        }

        public Table(string ston, bool hasHeader)
        {
            unserialize(ston);
            _hasHeader = hasHeader;
        }

        public string serialize()
        {
            List<string> escaped;
            StringBuilder sb = new StringBuilder();
            if (_hasHeader) {
                escaped = escapeList(_headers);
                sb.Append(CommonLang.ArrayHelper<string>.join(escaped.ToArray(), "\t"));
                sb.Append("\n");
            }
            for (int idx = 0; idx < _data.Count; idx++) {
                if (idx > 0) {
                    sb.Append("\n");
                }
                escaped = escapeList(_data[idx]);
                sb.Append(CommonLang.ArrayHelper<string>.join(escaped.ToArray(), "\t"));
            }
            return sb.ToString();
        }

        public void unserialize(string ston)
        {
            string[] lines = ston.Replace('\r', '\n').Replace("\n\n", "\n").Split('\n');
            if (_hasHeader)
            {
                if (lines.Length > 0)
                {
                    _headers = CommonLang.ArrayHelper<string>.toList(lines[0].Split('\t'));
                    for (int idx = 0; idx < _headers.Count; idx++)
                    {
                        _columnIndexMap[_headers[idx]] = idx;
                    }
                }
                _headers = unescapeList(_headers);
            }
            for (int i = _hasHeader ? 1 : 0; i < lines.Length; i++)
            {
            	if (lines[i].Trim(new char[]{'\t'}).Length == 0) {
            		continue;
            	}
                string[] cols = lines[i].Split('\t');
                _data.Add(CommonLang.ArrayHelper<string>.toList(cols));
            }
            // 转义 二维表
            List<List<string>> unescapedData = new List<List<string>>(_data.Count);
            foreach (List<string> row in _data)
            {
                unescapedData.Add(unescapeList(row));
            }
            _data = unescapedData;
        }

        public Dictionary<string, string> getDictionary(int row)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            foreach (string header in _headers) {
                ret[header] = _data[row][_columnIndexMap[header]];
            }
            return ret;
        }
        
        public string get(int row, string column)
        {
            return _data[row][_columnIndexMap[column]];
        }

        public void set(string column, int row, string value)
        {
            _data[row][_columnIndexMap[column]] = value;
        }

        public List<string> getRow(int row)
        {
            return _data[row];
        }

        public List<string> unescapeList(List<string> list)
        {
            List<string> ret = new List<string>(list.Count);
            foreach (string str in list)
            {
                ret.Add(unescape(str));
            }
            return ret;
        }

        public List<string> escapeList(List<string> list)
        {
            List<string> ret = new List<string>(list.Count);
            foreach (string str in list)
            {
                ret.Add(escape(str));
            }
            return ret;
        }

        public string unescape(string str)
        {
            str = str.Replace("\\rt", "\r");
            str = str.Replace("\\nl", "\n");
            str = str.Replace("\\tab", "\t");
            return str;
        }

        public string escape(string str)
        {
            str = str.Replace("\r", "\\rt");
            str = str.Replace("\n", "\\nl");
            str = str.Replace("\t", "\\tab");
            return str;
        }

    }
}

