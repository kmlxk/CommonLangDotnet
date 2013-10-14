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
            for (int i = 0; i < count; i++)
            {
                list.Add(val);
            }
        }

        public static int count(List<T> list, T val)
        {
            int ret = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(val))
                {
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
            foreach (T item in list)
            {
                genList.Add(item);
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
        public static TreeNode<T> listToTree<Tpk>(List<T> list,
            string pn_idField,
            string pn_textField,
            string pn_actionField,
            string pn_cssClassField,
            string pn_parentIdField)
        {
            return listToTree<Tpk>(list, pn_idField, pn_textField, pn_actionField, pn_cssClassField, pn_parentIdField, "");
        }

        public static TreeNode<T> listToTree<Tpk>(List<T> list, 
            string pn_idField, 
            string pn_textField, 
            string pn_actionField, 
            string pn_cssClassField,
            string pn_parentIdField,
            string pn_priorField) 
        {
            Type type = typeof(T);
            PropertyInfo idField = type.GetProperty(pn_idField);
            PropertyInfo textField = type.GetProperty(pn_textField);
            PropertyInfo actionField = type.GetProperty(pn_actionField);
            PropertyInfo cssClassField = type.GetProperty(pn_cssClassField);
            PropertyInfo parentIdField = type.GetProperty(pn_parentIdField);
            PropertyInfo priorField = type.GetProperty(pn_priorField);
            
            TreeNode<T> tree = new TreeNode<T>();

            // 多个根节点的情况下，需要新建一个根根节点
            TreeNode<T> rrNode = new TreeNode<T>();
            rrNode.children = new List<TreeNode<T>>();

            // convert to map
            Dictionary<Tpk, T> objMap = new Dictionary<Tpk, T>();
            foreach (T obj in list)
            {
                objMap.Add((Tpk)idField.GetValue(obj, null), obj);
            }
            Dictionary<Tpk, TreeNode<T>> nodeMap = new Dictionary<Tpk, TreeNode<T>>();
            // convert to node map
            foreach (T obj in list)
            {
                TreeNode<T> node = new TreeNode<T>(
                    (string)textField.GetValue(obj, null),
                    actionField != null ? (string)actionField.GetValue(obj, null) : "",
                    cssClassField != null ? (string)cssClassField.GetValue(obj, null) : "",
                    null,
                    obj,
                    (Tpk)idField.GetValue(obj, null),
                    priorField != null ? (int)priorField.GetValue(obj, null) : 0);
                nodeMap.Add((Tpk)idField.GetValue(obj, null), node);
            }

            // convert to map
            TreeNode<T> rootNode = null;
            foreach (T obj in list)
            {
                TreeNode<T> node = nodeMap[(Tpk)idField.GetValue(obj, null)];
                Tpk parentId = (Tpk)parentIdField.GetValue(node.data, null);
                if (Convert.ToString(parentId).Equals("0"))
                {
                    rootNode = node;
                    rrNode.children.Add(rootNode);
                }
                else
                {
                    //obsoleted: 如果级联删除失效，出现孤岛节点，则异常
                    //如果引用的父节点不在列表中，添加root.children
                    //if (parentId == null || !nodeMap.ContainsKey(parentId))
                    if (parentId == null || CommonLang.StringHelper.toInt(parentId) == 0)
                    {
                        rootNode = node;
                        continue;
                    }
                    // 如果孤岛节点在寻找上一节点，则跳过
                    if (!nodeMap.ContainsKey(parentId)) {
                        continue;
                    }
                    TreeNode<T> parent = nodeMap[parentId];
                    if (parent.children == null)
                    {
                        parent.children = new List<TreeNode<T>>();
                    }
                    parent.children.Add(node);
                }
            }
            if (rrNode.children.Count > 0)
            {
                TreeSort(rrNode);
                return rrNode;
            }
            TreeSort(rootNode);
            return rootNode;
        }

        public static void TreeSort(TreeNode<T> tree)
        {
            if (tree.children == null)
                return;
            foreach (TreeNode<T> subtree in tree.children)
            {
                if (subtree.children == null) {
                    continue;
                }
                tree.children.Sort(TreeNodeCompare);
            }
        }
        public static int TreeNodeCompare(TreeNode<T> t1, TreeNode<T> t2)
        {
            return t1.prior.CompareTo(t2.prior);
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
            foreach (T item in list)
            {
                if (field.GetValue(item).Equals(value))
                {
                    return item;
                }
            }
            return default(T);
        }

        public static List<T> searchField<T1>(List<T> list, string fieldName, T1 value)
        {
            List<T> ret = new List<T>();
            Type type = typeof(T);
            FieldInfo field = type.GetField(fieldName);
            foreach (T item in list)
            {
                if (field.GetValue(item).Equals(value))
                {
                    ret.Add(item);
                }
            }
            return ret;
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
        public static EasyuiTreeNode<Tpk, T> listToEasyuiTree<Tpk>(List<T> list,
    string pn_idField,
    string pn_textField,
    string pn_parentIdField)
        {
            Type type = typeof(T);
            PropertyInfo idField = type.GetProperty(pn_idField);
            PropertyInfo textField = type.GetProperty(pn_textField);
            PropertyInfo parentIdField = type.GetProperty(pn_parentIdField);

            EasyuiTreeNode<Tpk, T> tree = new EasyuiTreeNode<Tpk, T>();

            // 多个根节点的情况下，需要新建一个根根节点
            EasyuiTreeNode<Tpk, T> rrNode = new EasyuiTreeNode<Tpk, T>();
            rrNode.children = new List<EasyuiTreeNode<Tpk, T>>();

            // convert to map
            Dictionary<Tpk, T> objMap = new Dictionary<Tpk, T>();
            foreach (T obj in list)
            {
                objMap.Add((Tpk)idField.GetValue(obj, null), obj);
            }
            Dictionary<Tpk, EasyuiTreeNode<Tpk, T>> nodeMap = new Dictionary<Tpk, EasyuiTreeNode<Tpk, T>>();
            // convert to node map
            foreach (T obj in list)
            {
                EasyuiTreeNode<Tpk, T> node = new EasyuiTreeNode<Tpk, T>();
                node.text = (string)textField.GetValue(obj, null);
                node.attributes = obj;
                node.id = (Tpk)idField.GetValue(obj, null);
                nodeMap.Add((Tpk)idField.GetValue(obj, null), node);
            }

            // convert to map
            EasyuiTreeNode<Tpk, T> rootNode = null;
            foreach (T obj in list)
            {
                EasyuiTreeNode<Tpk, T> node = nodeMap[(Tpk)idField.GetValue(obj, null)];
                Tpk parentId = (Tpk)parentIdField.GetValue(node.attributes, null);
                if (parentId == null || Convert.ToString(parentId).Equals("0"))
                {
                    rootNode = node;
                    rrNode.children.Add(rootNode);
                }
                else
                {
                    // 如果引用的父节点不在列表中，添加root.children
                    if (parentId == null || !nodeMap.ContainsKey(parentId))
                    {
                        rootNode = node;
                        continue;
                    }
                    EasyuiTreeNode<Tpk, T> parent = nodeMap[parentId];
                    if (parent.children == null)
                    {
                        parent.children = new List<EasyuiTreeNode<Tpk, T>>();
                    }
                    parent.children.Add(node);
                }
            }
            if (rrNode.children.Count > 0)
            {
                return rrNode;
            }
            return rootNode;
        }
    }
}
