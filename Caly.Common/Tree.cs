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

    public TreeNode AddChild(TreeNode child)
    {
        if (child.Parent != null)
        {
            child.Parent.Children.Remove(child);
        }

        child.Parent = this;

        Children.Add(child);

        return child;
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

    public virtual bool IsLeaf()
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

    public TreeNode(V leaf)
    {
        Leaf = leaf;
    }

    public override bool IsLeaf()
    {
        return Leaf != null;
    }

    public TreeNode<T,V> AddChild(T value)
    {
        var node = new TreeNode<T,V>(value);
        base.AddChild(node);
        return node;
    }
    public TreeNode<T, V> AddChild(V leaf)
    {
        var node = new TreeNode<T,V>(leaf);
        base.AddChild(node);
        return node;
    }

    public List<T> BuildChainUp(List<T> chain, Guid id=default(Guid))
    {
        if (chain==null)
        {
            chain = new List<T>();
        }

        if (this.Id != id || id==Guid.Empty)
        {
            if (!this.IsLeaf())
            {
                chain.Add(this.Value);
            }
            if (this.Parent!=null)
            {
                chain = (Parent as TreeNode<T, V>).BuildChainUp(chain, id);
            }
        }

        return chain;
    }

    public T Value
    {
        get;
        set;
    }

    public V Leaf
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the leaves hanging from the nodes, recursively
    /// </summary>
    /// <returns>The leaves.</returns>
    /// <param name="leaves">Leaves.</param>
    public List<TreeNode<T,V>> GetLeaves(List<TreeNode<T,V>> leaves=null)
    {
        if (leaves == null)
        {
            leaves = new List<TreeNode<T,V>>();
        }

        if (IsLeaf())
        {
            leaves.Add(this);
            return leaves;
        }

        Children.ConvertAll(x => (TreeNode<T, V>)x).ForEach(x => leaves = x.GetLeaves(leaves));

        return leaves;
    }

    /// <summary>
    /// Narrows the tree according to the criteria, recursively. Multiple nodes can meet the criteria. Eg: you know its a street type and you want the street type returned. Criteria will be: [{Order:1, Name: Address}, {Order:2, Name:StreetType}}]
    /// </summary>
    /// <returns>The narrow.</returns>
    /// <param name="nodelist">Nodelist.</param>
    /// <param name="criteria">Criteria.</param>
    /// <param name="passFurther">Pass further down the tree. It means that the node is in the criteria but s not of the lowest depth </param>
    /// <param name="acquire">Acquire the node when the lowest depth is reached and is in criteia</param>
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