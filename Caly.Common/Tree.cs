using System;
using System.Collections.Generic;

public class TreeNode : IEquatable<TreeNode>
{
    public TreeNode()
    {
        Children = new List<TreeNode>();
        Id = Guid.NewGuid();
    }

    public Guid Id
    {
        get;
        private set;
    }

    public TreeNode Parent
    {
        get;
        set;
    }

    public List<TreeNode> Children
    {
        get;
        private set;
    }

    public void AddChild(TreeNode child)
    {
        if (child.Parent != null)
        {
            child.Parent.Children.Remove(child);
        }

        child.Parent = this;

        Children.Add(child);
    }

    public bool Equals(TreeNode other)
    {
        return (Id == other.Id);
    }

    public int Depth(int prev = 0)
    {
        if (Parent == null)
        {
            return prev;
        }

        return Parent.Depth(prev++);
    }

    public virtual bool IsLeafe()
    {
        return false;
    }

}

public class TreeNode<T, V> : TreeNode
{

    public TreeNode(T value)
    {
        Value = value;
    }

    public TreeNode(V leafe)
    {
        Leafe = leafe;
    }

    public override bool IsLeafe()
    {
        return Leafe != null;
    }

    public T Value
    {
        get;
        set;
    }

    public V Leafe
    {
        get;
        private set;
    }

    public List<V> GetLeaves(List<V> leaves=null)
    {
        if (leaves == null)
        {
            leaves = new List<V>();
        }

        if (IsLeafe())
        {
            leaves.Add(this.Leafe);
            return leaves;
        }

        Children.ConvertAll(x => (TreeNode<T, V>)x).ForEach(x => leaves = x.GetLeaves(leaves));

        return leaves;
    }

    public List<TreeNode<T, V>> Narrow(List<TreeNode<T,V>> nodelist, List<T> criteria, Func<TreeNode<T,V>, bool> passFurther, Func<TreeNode<T, V>, bool> acquire)
    {


        if (nodelist==null)
        {
            nodelist = new List<TreeNode<T, V>>();
        }
        if (criteria==null)
        {
            return nodelist;
        }

        if(acquire(this))
        {
            nodelist.Add(this);
            return nodelist;
        }

        if (passFurther(this))
        {
            Children.ConvertAll(x => (TreeNode<T, V>)x).ForEach(x => nodelist = x.Narrow(nodelist, criteria, passFurther, acquire));
            return nodelist;
        }

        return nodelist;
    }

}

public class Tree<T, V>
{
    public Tree()
    {

    }
    public Tree(T value)
    {
        Root = new TreeNode<T, V>(value);
    }
    public TreeNode<T, V> Root { get; set; }
}