using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLang
{
    public class TreeNode<T>
    {
        public List<TreeNode<T>> children;
        public string text;
        public string action;
        public string cssClass;
        public T data;

        public TreeNode()
        { 
        }

        public TreeNode(string text, string action, string cssClass, List<TreeNode<T>> children, T data)
        {
            this.children = children;
            this.text = text;
            this.action = action;
            this.data = data;
            this.cssClass = cssClass;
        }
    }
}
