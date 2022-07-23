using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1060DS
{
    public class BST<T> :IEnumerable <T> where T : IComparable<T>
    {
        Node root;
        public Node RootProp { get => root;}

        public Node Add(T item) // O(logN)
        {
            if (root == null)
            {
                root = new Node(item);
                return root;
            }

            Node tmp = root;
            var newNode = new Node(item);
            while (true)
            {
                if (item.CompareTo(tmp.value) < 0) // item < tmp.value - go left
                {
                    if (tmp.Left == null)
                    {
                        tmp.Left = newNode;
                        break;
                    }
                    else tmp = tmp.Left;
                }
                else // go right
                {
                    if (tmp.Right == null)
                    {
                        tmp.Right = newNode;
                        break;
                    }
                    else tmp = tmp.Right;
                }
            }
            return newNode;
            // count++
            //+ notification
        }
        public bool Remove(T key)
        {
            Node parent = root;
            Node current = root;
            bool leftChildren = false;
            while (current != null && current.value.CompareTo(key) != 0)
            {
                parent = current;
                if (key.CompareTo(current.value) < 0)
                {
                    current = current.Left;
                    leftChildren = true;
                }
                else
                {
                    current = current.Right;
                    leftChildren = false;
                }
            }
            if (current == null) return false;
            if (current.Left == null && current.Right == null)
            {
                if (current == root)
                    root = null;
                else if (leftChildren)
                    parent.Left = null;
                else
                    parent.Right = null;
            }
            else if (current.Right == null)
            {
                if (current == root)
                    root = current.Left;
                else if (leftChildren)
                    parent.Left = current.Left;
                else
                    parent.Right = current.Right;

            }
            else if (current.Left == null)
            {
                if (current == root)
                    root = current.Right;
                else if (leftChildren)
                    parent.Left = parent.Right;
                else
                    parent.Right = current.Right;
            }
            else
            {
                Node replacement = GetReplacement(current);

                if (current == root)
                    root = replacement;
                else if (leftChildren)
                    parent.Left = replacement;

                else
                {
                    parent.Right = replacement;
                    replacement.Left = current.Left;
                }
            }
            return true;
        }
        public Node GetReplacement(Node tmpNode)
        {
            Node replacementParent = tmpNode;
            Node replacement = tmpNode;
            Node tmp = tmpNode.Right;
            while (!(tmp == null))
            {
                replacementParent = tmp;
                replacement = tmp;
                tmp = tmp.Left;
            }
            if (!(replacement == tmpNode.Right))
            {
                replacementParent.Left = replacement.Right;
                replacement.Right = tmpNode.Right;
            }
            return replacement;
        }

        public bool Search(T item, out T foundItem)
        {
            Node tmp = root;
            while (tmp != null)
            {
                if (item.CompareTo(tmp.value) == 0)
                {
                    foundItem = tmp.value;
                    return true;
                }
                if (item.CompareTo(tmp.value) < 0) tmp = tmp.Left;
                else tmp = tmp.Right;
            }
            foundItem = default;
            return false;
        }

        public bool SearchBestMatch(T item, out T foundItem )
        {
            Node tmp = root;
            Node tmpHigher = tmp;
            int counter = 0;
            while (tmp != null)
            {
                if (item.CompareTo(tmp.value) == 0)
                {
                    foundItem = tmp.value;
                    return true;
                }
                if (item.CompareTo(tmp.value) >= 0) tmp = tmp.Right;
                else
                {
                    tmpHigher = tmp;
                    tmp = tmp.Left;
                    counter++;
                }
            }
            if (counter > 0)
            {
                foundItem = tmpHigher.value;
                return true;
            }
            foundItem = default;
            return false;
        }

        public int GetLevelsCnt()
        {
            return GetLevelsCnt(root);
        }
        int GetLevelsCnt(Node subThreeRoot)
        {
            if (subThreeRoot == null) return 0;
            return Math.Max(GetLevelsCnt(subThreeRoot.Left), GetLevelsCnt(subThreeRoot.Right)) + 1;
        }
        public void ScanInOrder(Action<T> singleItemAction)
        {
            ScanInOrder(root, singleItemAction);
        }
        void ScanInOrder(Node subThreeRoot, Action<T> singleItemAction)
        {
            if (subThreeRoot == null) return;
            ScanInOrder(subThreeRoot.Left, singleItemAction);
            singleItemAction(subThreeRoot.value);
            ScanInOrder(subThreeRoot.Right, singleItemAction);
        }

        public IEnumerator<T> GetEnumerator()
        {
           return root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node : IEnumerable <T>
        {
            public T value;
            public Node Left;
            public Node Right;
           

            public Node(T value)
            {
                this.value = value;
                Left = Right = null;
            }

            public IEnumerator<T> GetEnumerator()
            {

                if (Left != null)
                {
                    foreach (var item in Left)
                    {
                        yield return item;
                    }
                }
                yield return value;

                if (Right != null)
                {
                    foreach (var item in Right)
                    {
                        yield return item;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
