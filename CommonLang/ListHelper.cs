/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2013/6/23
 * Time: 8:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CommonLang
{
	/// <summary>
	/// Description of ListHelper.
	/// </summary>
    public class ListHelper<T>
	{
		public ListHelper()
		{
		}
		
		/// <summary>
		/// 向列表填充数据
		/// </summary>
		/// <param name="list"></param>
		/// <param name="val"></param>
		/// <param name="count"></param>
		public static void fill(List<T> list, T val, int count)
		{
			for (int i = 0; i < count; i++) {
				list.Add(val);
			}
		}
		
		public static int count(List<T> list, T val)
		{
			int ret = 0;
			for (int i = 0; i < list.Count; i++) {
				if (list[i].Equals(val)) {
					ret++;
				}
			}
			return ret;
		}

        /// <summary>
        /// 将IList转换为泛型的List
        /// </summary>
        public static List<T> fromIList(System.Collections.IList list)
        {
            List<T> genList = new List<T>();
            foreach (T doc in list)
            {
                genList.Add(doc);
            }
            return genList;
        }     


        /// <summary>
        /// 转换平面的List<T>为树形对象
        /// </summary>
        /// <param name="list">System.Collections.Generic.List</param>
        /// <param name="pn_idField">ID字段名称</param>
        /// <param name="pn_textField">text字段名称</param>
        /// <param name="pn_actionField">action字段名称</param>
        /// <param name="pn_parentIdField">parentId字段名称</param>
        /// <returns></returns>
        public static TreeNode<T> listToTree(List<T> list, string pn_idField, string pn_textField, string pn_actionField, string pn_cssClassField, string pn_parentIdField)
        {
            Type type = typeof(T);
            PropertyInfo idField = type.GetProperty(pn_idField);
            PropertyInfo textField = type.GetProperty(pn_textField);
            PropertyInfo actionField = type.GetProperty(pn_actionField);
            PropertyInfo cssClassField = type.GetProperty(pn_cssClassField);
            PropertyInfo parentIdField = type.GetProperty(pn_parentIdField);
            
            TreeNode<T> tree = new TreeNode<T>();

            // convert to map
            Dictionary<long, T> objMap = new Dictionary<long, T>();
            foreach (T obj in list)
            {
                objMap.Add((int)idField.GetValue(obj, null), obj);
            }
            Dictionary<long, TreeNode<T>> nodeMap = new Dictionary<long, TreeNode<T>>();
            // convert to node map
            foreach (T obj in list)
            {
                TreeNode<T> node = new TreeNode<T>(
                    (string)textField.GetValue(obj, null), 
                    (string)actionField.GetValue(obj, null),
                    (string)cssClassField.GetValue(obj, null), 
                    null, 
                    obj);
                nodeMap.Add((int)idField.GetValue(obj, null), node);
            }

            // convert to map
            TreeNode<T> rootNode = null;
            foreach (T obj in list)
            {
                TreeNode<T> node = nodeMap[(int)idField.GetValue(obj, null)];
                int parentId = (int)parentIdField.GetValue(node.data, null);
                if (parentId <= 0)
                {
                    rootNode = node;
                }
                else
                {
                    TreeNode<T> parent = nodeMap[parentId];
                    if (parent.children == null)
                    {
                        parent.children = new List<TreeNode<T>>();
                    }
                    parent.children.Add(node);
                }
            }
            return rootNode;
        }

        public static string join(List<T> list)
        {
            // 预申请空间可以提升效率
            int capacity = list.Count;
            if (list.Count > 0)
            {
                capacity = list[0].ToString().Length * list.Count;
            }
            if (capacity <= 0)
            {
                capacity = 10;
            }
            // 各元素ToString后拼接字符串
            StringBuilder sb = new StringBuilder(capacity);
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(list[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将List中的数据，逐个拼接
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="separator">分隔字符串</param>
        /// <returns>拼接字符串</returns>
        public static string join(List<T> list, string separator)
        {
            // 预申请空间可以提升效率
            int capacity = list.Count;
            if (list.Count > 0)
            {
                capacity = (list[0].ToString().Length + separator.Length) * list.Count;
            }
            if (capacity <= 0)
            {
                capacity = 10;
            }
            // 各元素ToString后拼接字符串
            StringBuilder sb = new StringBuilder(capacity);
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(list[i].ToString());
            }
            string ret = sb.ToString();
            return ret;
        }

        public static T findField<T1>(List<T> list, string fieldName, T1 value)
        {
            Type type = typeof(T);
            FieldInfo field = type.GetField(fieldName);
            foreach (T item in list) {
                if (field.GetValue(item).Equals(value)) {
                    return item;
                }
            }
            return default(T);
        }
    }
}
