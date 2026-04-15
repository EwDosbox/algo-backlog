namespace Trees
{
    #region Search Tree
    public abstract class SearchTree<TNode> where TNode : Node<TNode>, INilNode<TNode>
    {
        protected TNode root;

        public TNode Root => root;

        protected SearchTree(TNode root)
        {
            this.root = root;
        }

        public bool Contains(int x)
        {
            TNode node = root;

            while (node != TNode.Nil)
            {
                if (node.Value == x)
                    return true;

                if (x > node.Value)
                    node = node.Right;
                else
                    node = node.Left;
            }
            return false;
        }

        public TNode Minimum()
        {
            TNode node = root;

            while (node.Left != TNode.Nil)
                node = node.Left;

            return node;
        }

        public TNode Minimum(TNode node)
        {
            while (node.Left != TNode.Nil)
                node = node.Left;
            return node;
        }

        public TNode Maximum()
        {
            TNode node = root;

            while (node.Right != TNode.Nil)
                node = node.Right;

            return node;
        }

        public TNode Maximum(TNode node)
        {
            TNode curr = node;
            while (curr.Right != TNode.Nil)
                curr = curr.Right;
            return curr;
        }

        public TNode Predecessor(TNode node)
        {
            if (node.Left != TNode.Nil)
                return Maximum(node.Left);

            TNode x = node;
            TNode y = x.Parent;

            while (y != TNode.Nil && y.Left == x)
            {
                x = y;
                y = x.Parent;
            }

            return y;
        }

        public TNode Successor(TNode node)
        {
            if (node.Right != TNode.Nil)
                return Minimum(node.Right);

            TNode x = node;
            TNode y = x.Parent;

            while (y != TNode.Nil && y.Right == x)
            {
                x = y;
                y = x.Parent;
            }

            return y;
        }

        protected void Link(TNode parent, TNode child, bool isLeft)
        {
            if (parent != TNode.Nil)
            {
                if (isLeft)
                    parent.Left = child;
                else
                    parent.Right = child;
            }

            if (child != TNode.Nil)
                child.Parent = parent;
        }

        protected void Transplant(TNode oldNode, TNode newNode)
        {
            if (oldNode.Parent == TNode.Nil)
                root = newNode;
            else if (oldNode == oldNode.Parent.Left)
                oldNode.Parent.Left = newNode;
            else
                oldNode.Parent.Right = newNode;

            if (newNode != TNode.Nil)
                newNode.Parent = oldNode.Parent;
        }
        public bool IsCorrect()
        {
            int[] treeRep = root.Represent(Order.InOrder);

            return General.IsSorted(treeRep);
        }

        public virtual void Insert(TNode node)
        {
            TNode x = root;

            while (x != TNode.Nil)
            {
                if (node.Value > x.Value)
                {
                    if (x.Right == TNode.Nil)
                    {
                        Link(x, node, false);
                        return;
                    }
                    x = x.Right;
                }
                else
                {
                    if (x.Left == TNode.Nil)
                    {
                        Link(x, node, true);
                        return;
                    }
                    x = x.Left;
                }
            }
        }

        public virtual void Delete(TNode node)
        {
            if (node.Left == TNode.Nil)
                Transplant(node, node.Right);

            else if (node.Right == TNode.Nil)
                Transplant(node, node.Left);

            else
            {
                TNode successor = Minimum(node.Right);

                if (successor.Parent != node)
                {
                    Transplant(successor, successor.Right);
                    successor.Right = node.Right;
                    successor.Right.Parent = successor;
                }

                Transplant(node, successor);
                successor.Left = node.Left;
                successor.Left.Parent = successor;
            }
        }

    }
    #endregion
    #region Binary Search Tree
    public class BST : SearchTree<BinNode>
    {
        public BST(BinNode root) : base(root) { }

        public override string ToString() => root.ToString(Order.InOrder);
    }
    #endregion
    #region Red Black Tree
    public class RBTree : SearchTree<RBNode>
    {
        public RBTree(RBNode root) : base(root) { }

        public override void Delete(RBNode a)
        {
            switch (a.NoOfChildren)
            {
                case 0:
                    {
                        if (a == root)
                        {
                            root = RBNode.Nil;
                            return;
                        }
                        RBNode b = a.Parent;

                        if (a.isLeftChild)
                            b.Left = RBNode.Nil;
                        else
                            b.Right = RBNode.Nil;

                        if (a.IsBlack)
                        {
                            RBNode.Nil.Parent = b;
                            TwoColors(RBNode.Nil, NodeColor.Black, NodeColor.Black);
                            RBNode.Nil.Parent = RBNode.Nil;
                        }

                        break;
                    }
                case 1:
                    {
                        RBNode b = (a.Left == RBNode.Nil) ? a.Right : a.Left;
                        Transplant(a, b);
                        if (a.IsBlack)
                            b.Color = NodeColor.Black;
                        break;
                    }
                case 2:
                    {
                        RBNode d = Successor(a);
                        a.Value = d.Value;
                        Delete(d);
                        break;
                    }

            }
        }
        private void TwoColors(RBNode a, NodeColor prev, NodeColor curr)
        {
            if (prev == NodeColor.Red && curr == NodeColor.Black ||
                prev == NodeColor.Black && curr == NodeColor.Red)
            {
                a.Color = NodeColor.Black;
                return;
            }
            if (prev == NodeColor.Black && curr == NodeColor.Black)
            {
                RBNode c = a.Sibling;
                RBNode b = a.Parent;
                if (c.IsRed)
                {
                    if (a.isLeftChild)
                        LeftRotate(b);
                    else
                        RightRotate(b);

                    (b.Color, c.Color) = (c.Color, b.Color);
                    TwoColors(a, prev, curr);
                    return;
                }
                RBNode near = a.isLeftChild ? c.Left : c.Right;
                RBNode far = a.isLeftChild ? c.Right : c.Left;
                if (c.IsBlack && near.IsBlack && far.IsBlack)
                {
                    if (a == root)
                        return;
                    c.Color = NodeColor.Red;
                    TwoColors(b, b.Color, NodeColor.Black);
                    return;
                }
                if (c.IsBlack && near.IsRed && far.IsRed)
                {
                    if (a.isLeftChild)
                        RightRotate(c);
                    else
                        LeftRotate(c);
                    (near.Color, c.Color) = (c.Color, near.Color);
                    TwoColorsCase4(b, near, c, a.isLeftChild);
                    return;
                }
                if (c.IsBlack && far.IsRed)
                {
                    TwoColorsCase4(b, c, far, a.isLeftChild);
                    return;
                }
            }
        }
        private void TwoColorsCase4(RBNode b, RBNode c, RBNode e, bool aIsLeft)
        {
            NodeColor bOldColor = b.Color;
            if (aIsLeft) LeftRotate(b);
            else RightRotate(b);
            c.Color = bOldColor;
            b.Color = NodeColor.Black;
            e.Color = NodeColor.Black;
        }

        public void Insert(int value)
        {
            RBNode node = new(value);
            node.Left = RBNode.Nil;
            node.Right = RBNode.Nil;

            Insert(node);
        }
        public override void Insert(RBNode node)
        {
            base.Insert(node);
            node.Color = NodeColor.Red;
            FixTree(node);
        }
        private void FixTree(RBNode a)
        {
            RBNode b = a.Parent;
            if (b == RBNode.Nil || b.IsBlack)
                return;

            RBNode d = b.Pibling;
            RBNode c = a.Parent.Parent;

            if (a.IsRed && b.IsRed && d.IsRed && c.IsBlack)
            {
                b.Color = NodeColor.Black;
                d.Color = NodeColor.Black;
                c.Color = NodeColor.Red;

                if (c == root)
                {
                    c.Color = NodeColor.Black;
                    return;
                }
                FixTree(c);

                return;
            }
            if (a.IsRed && a.isRightChild && b.IsRed && b.isLeftChild && d.IsBlack && c.IsBlack)
            {
                LeftRotate(b);
                FixCase3(a, b, d, c);
                return;
            }
            if (a.IsRed && a.isLeftChild && b.IsRed && b.isLeftChild && d.IsBlack && c.IsBlack)
            {
                FixCase3(a, b, d, c);
                return;
            }

            if (a.IsRed && a.isLeftChild && b.IsRed && b.isRightChild && d.IsBlack && c.IsBlack)
            {
                RightRotate(b);
                FixCase3Mirror(a, b, d, c);
                return;
            }
            if (a.IsRed && a.isRightChild && b.IsRed && b.isRightChild && d.IsBlack && c.IsBlack)
            {
                FixCase3Mirror(a, b, d, c);
                return;
            }
        }
        private void FixCase3(RBNode a, RBNode b, RBNode d, RBNode c)
        {
            RightRotate(c);
            (b.Color, c.Color) = (c.Color, b.Color);
        }
        private void FixCase3Mirror(RBNode a, RBNode b, RBNode d, RBNode c)
        {
            LeftRotate(c);
            (b.Color, c.Color) = (c.Color, b.Color);
        }

        private void LeftRotate(RBNode x)
        {
            RBNode y = x.Right;
            if (y == RBNode.Nil)
                return;

            Link(x, y.Left, false);

            if (x == root)
            {
                root = y;
                y.Parent = RBNode.Nil;
            }
            else
                Link(x.Parent, y, x.Parent.Left == x);

            Link(y, x, true);
        }

        private void RightRotate(RBNode y)
        {
            RBNode x = y.Left;
            if (x == RBNode.Nil)
                return;

            Link(y, x.Right, true);

            if (y == root)
            {
                root = x;
                x.Parent = RBNode.Nil;
            }
            else
                Link(y.Parent, x, y.Parent.Left == y);

            Link(x, y, false);
        }
    }
    #endregion
}