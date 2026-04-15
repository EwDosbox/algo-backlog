using System.Drawing;

namespace Trees
{
    #region Interface for Nil
    public interface INilNode<TSelf> where TSelf : INilNode<TSelf>
    {
        static abstract TSelf Nil { get; }
    }
    #endregion
    #region Generic Binary Node Class
    public abstract class Node<TNode> where TNode : Node<TNode>, INilNode<TNode>
    {
        public int Value { get; set; }

        public TNode Left { get; set; }
        public TNode Right { get; set; }
        public TNode Parent { get; set; }
        public TNode Pibling
        {
            get
            {
                if (Parent.Left == this)
                    return Parent.Right;
                else if (Parent.Right == this)
                    return Parent.Left;
                else
                    return TNode.Nil;
            }
        }
        public TNode Sibling
        {
            get
            {
                return Pibling;
            }
        }
        public bool isLeftChild
        {
            get
            {
                return Parent.Left == this;
            }
        }
        public bool isRightChild
        {
            get
            {
                return Parent.Right == this;
            }
        }
        public int NoOfChildren
        {
            get
            {
                int noOfChildren = 0;

                if (Left != TNode.Nil)
                    noOfChildren++;
                if (Right != TNode.Nil)
                    noOfChildren++;

                return noOfChildren;
            }
        }

        public int Depth { get { return maxDepth(this); } }

        protected Node(int value)
        {
            Value = value;
            Left = TNode.Nil;
            Right = TNode.Nil;
            Parent = TNode.Nil;
        }
        protected Node()
        {
            Value = default;
        }

        private int maxDepth(Node<TNode> node)
        {
            if (node == TNode.Nil)
                return 0;

            int leftDepth = maxDepth(node.Left);
            int rightDepth = maxDepth(node.Right);

            return ((leftDepth > rightDepth) ? leftDepth : rightDepth) + 1;
        }

        public int[] Represent(Order order)
        {
            List<int> res = new();
            Represent(order, (TNode)this, res);
            return res.ToArray();
        }
        protected void Represent(Order order, TNode node, List<int> res)
        {
            if (node == TNode.Nil)
                return;
            switch (order)
            {
                case Order.PreOrder:
                    {
                        res.Add(node.Value);
                        Represent(order, node.Left, res);
                        Represent(order, node.Right, res);
                        break;
                    }
                case Order.InOrder:
                    {
                        Represent(order, node.Left, res);
                        res.Add(node.Value);
                        Represent(order, node.Right, res);
                        break;
                    }
                case Order.PostOrder:
                    {
                        Represent(order, node.Left, res);
                        Represent(order, node.Right, res);
                        res.Add(node.Value);
                        break;
                    }
            }
        }

        public string ToString(Order order)
        {
            switch (order)
            {
                case Order.PreOrder:
                    return $"{Value} " +
                           (Left != TNode.Nil ? Left.ToString(order) : "") +
                           (Right != TNode.Nil ? Right.ToString(order) : "");

                case Order.InOrder:
                    return (Left != TNode.Nil ? Left.ToString(order) : "") +
                           $"{Value} " +
                           (Right != TNode.Nil ? Right.ToString(order) : "");

                case Order.PostOrder:
                    return (Left != TNode.Nil ? Left.ToString(order) : "") +
                           (Right != TNode.Nil ? Right.ToString(order) : "") +
                           $"{Value} ";

                default:
                    return "";
            }
        }
    }
    #endregion
    #region Binary Node
    public class BinNode : Node<BinNode>, INilNode<BinNode>
    {
        private static readonly BinNode nil = new();
        public static BinNode Nil => nil;
        private BinNode() : base() { }
        public BinNode(int value, BinNode? l = null, BinNode? r = null, BinNode? p = null) : base(value)
        {
            Left = l ?? Nil;
            if (Left != Nil)
                Left.Parent = this;

            Right = r ?? Nil;
            if (Right != Nil)
                Right.Parent = this;

            Parent = p ?? Nil;
        }
    }
    #endregion
    #region Red Black Node
    public class RBNode : Node<RBNode>, INilNode<RBNode>
    {
        private NodeColor color;

        private static readonly RBNode nil = new();
        public static RBNode Nil => nil;

        public NodeColor Color
        {
            get { return color; }
            set
            {
                if (this == Nil)
                    return;
                color = value;
            }
        }
        public bool IsRed { get { return Color == NodeColor.Red; } }
        public bool IsBlack { get { return Color == NodeColor.Black; } }

        private RBNode() : base()
        {
            Color = NodeColor.Black;
        }

        public RBNode(int value, RBNode? parent = null) : base(value)
        {
            Color = NodeColor.Red;
            Parent = parent ?? Nil;
            Left = Nil;
            Right = Nil;
        }

        public override string ToString() => $"{Value} ({Color})";
    }
    #endregion
}
