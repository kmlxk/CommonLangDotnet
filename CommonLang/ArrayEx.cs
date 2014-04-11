using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLang
{


    /// <summary>
    /// 模仿PHP/JS的数组，融合了List和Dictionary的操作
    /// </summary>
    public class ArrayEx
    {

        protected Dictionary<string, object> _dict = null;
        protected List<object> _list = null;

        public ArrayEx()
        {

        }

        public void import(Dictionary<string, object> dict)
        {
            _dict = dict;
        }
        public void import(List<object> list)
        {
            _list = list;
        }

        public bool isDictionary()
        {
            return _dict != null;
        }

        List<object> getList()
        {
            return _list;
        }

        Dictionary<string, object> getDictionary()
        {
            return _dict;
        }
        public void set(string key, object value)
        {
            if (_list != null)
            {
                throw new ArrayTypeMismatchException();
            }
            if (_dict == null)
            {
                _dict = new Dictionary<string, object>();
            }
            _dict[key] = value;

        }

        public void set(object value)
        {
            if (_dict != null)
            {
                throw new ArrayTypeMismatchException();
            }
            if (_list == null)
            {
                _list = new List<object>();
            }
            _list.Add(value);

        }

        public void set(int index, object value)
        {
            if (_dict != null)
            {
                throw new ArrayTypeMismatchException();
            }
            if (_list == null)
            {
                _list = new List<object>();
            }
            _list[index] = value;

        }

        public object get(string key)
        {
            if (_dict == null)
            {
                throw new ArrayTypeMismatchException();
            }
            return _dict[key];
        }

        public object get(int index)
        {
            if (_list == null)
            {
                throw new ArrayTypeMismatchException();
            }
            return _list[index];
        }

        public int Count
        {
            get
            {
                if (_dict != null)
                    return _dict.Count;
                if (_list != null)
                    return _list.Count;
                return 0;
            }
        }

        public static List<object> getCols(ArrayEx arrayEx, string key)
        {
            List<object> ret = new List<object>();
            for (int i = 0; i < arrayEx.Count; i++)
            {
                object item = arrayEx.get(i);
                ArrayEx child = (ArrayEx)item;
                ret.Add(child.get(key));
            }
            return ret;
        }


        /// <summary>
        /// 在主对象中插入关联的子对象。
        /// 通常用于ORM处理，例如：分别查询出主表的记录、相关联表的记录，然后根据主键和外键，关联到主表的记录中。
        /// </summary>
        /// <param name="array">主表的数组</param>
        /// <param name="childArray">关联表的数组</param>
        /// <param name="key">主表中的外键</param>
        /// <param name="childKey">关联表的主键</param>
        /// <param name="assocField">主对象中的子属性</param>
        public static void associate(ArrayEx array, ArrayEx childArray, string key, string childKey, string assocField)
        {
            for (int i = 0; i < array.Count; i++)
            {
                object item = array.get(i);
                ArrayEx arItem = (ArrayEx)item;
                ArrayEx associated = ArrayEx.searchField(childArray, childKey, arItem.get(key));
                if (associated == null)
                {
                    continue;
                }
                arItem.set(assocField, associated);
            }
        }

        public static ArrayEx searchField(ArrayEx array, string key, object value)
        {
            for (int i = 0; i < array.Count; i++)
            {
                object item = array.get(i);
                ArrayEx arItem = (ArrayEx)item;
                if (arItem.get(key).Equals(value))
                {
                    return arItem;
                }
            }
            return null;
        }


        public static ArrayEx from(Dictionary<string, object> dict)
        {
            ArrayEx ret = new ArrayEx();
            ret.import(dict);
            return ret;
        }

        public static ArrayEx from(List<object> list)
        {
            ArrayEx ret = new ArrayEx();
            ret.import(list);
            return ret;
        }

        public object getRawType()
        {
            if (isDictionary())
            {
                return convertToDictionary();
            }
            else
            {
                return convertToList();
            }
        }

        public List<object> convertToList()
        {
            List<object> ret = new List<object>(this.Count);
            for (int i = 0; i < _list.Count; i++)
            {
                object item = _list[i];
                if (item != null && item.GetType().Equals(typeof(ArrayEx)))
                {
                    ArrayEx arItem = (ArrayEx)item;
                    ret.Add(arItem.getRawType());
                }
                else
                {
                    ret.Add(item);
                }
            }
            return ret;
        }

        public Dictionary<string, object> convertToDictionary()
        {
            Dictionary<string, object> ret = new Dictionary<string, object>(this.Count);
            foreach (KeyValuePair<string, object> kv in _dict)
            {
                object item = kv.Value;
                if (item !=null && item.GetType().Equals(typeof(ArrayEx)))
                {
                    ArrayEx arItem = (ArrayEx)item;
                    ret.Add(kv.Key, arItem.getRawType());
                }
                else
                {
                    ret.Add(kv.Key, item);
                }
            }
            return ret;
        }

        public static void registerFastJson()
        {
            fastJSON.JSON.Instance.RegisterCustomType(typeof(ArrayEx), toJson, fromJson);
        }

        public static string toJson(object data)
        {
            ArrayEx array = (ArrayEx)data;
            string json = fastJSON.JSON.Instance.ToJSON(array.getRawType());
            return json;
        }

        public static object fromJson(string data)
        {
            return null;
        }

    }
}
