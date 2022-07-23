using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LinkedListAssignment
{
    public class DoubleLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        Node first = null;
        Node last = null;
        public Node First { get => first; }
        public Node Last { get => last; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = first;

            while (tmp != null)
            {
                sb.Append($"{tmp.Value} ");
                tmp = tmp.Next;
            }
            return sb.ToString();
        }
        public void AddFirst(T value)
        {
            Node node = new Node(value);
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                first.Previous = node;
                node.Next = first;
                first = node;
            }
        }
        public void RemoveFirst()
        {
            if (first == null)
                return;
            first = first.Next;
            first.Previous = null;
            if (first == null)
            {
                last = null;
            }
        }
        public void AddLast(T value)
        {
            if (first == null)
            {
                AddFirst(value);
                return;
            }

            Node n = new Node(value);
            last.Next = n;
            n.Previous = last;
            last = n;
        }

        public void RemoveLast()
        {
            if (this.first != null)
            {
                if (this.first.Next == null)
                {
                    this.first = null;
                }
                else
                {
                    Node temp;
                    temp = this.first;
                    while (temp.Next.Next != null)
                        temp = temp.Next;
                    Node lastNode = temp.Next;
                    temp.Next = null;
                    lastNode = null;
                }
            }

        }

        public bool AddAt(int index, T change)
        {
            Node tmp = first;
            Node node = new Node(change);
            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    node.Next = tmp.Next;
                    tmp.Next = node;
                    return true;
                }
                tmp = tmp.Next;
                if (tmp == null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool GetAt(int index, out T value)
        {
            Node current = first;
            for (int i = 0; i <= index; i++)
            {
                if (i == index)
                {
                    value = current.Value;
                    return true;
                }
                current = current.Next;
                if (current == null)
                {
                    value = default;
                    return false;
                }
            }
            value = current.Value;
            return false;
        }

        public void ChangeNodeLocation(Node nodes)
        {
            if (nodes == first)
            {
                return;
            }
            if (nodes == last)
            {
                last = nodes.Previous;
                AddFirst(nodes.Value);
                return;
            }
            if (nodes == null)
            {
                return;
            }
            nodes.Next.Previous = nodes.Previous;
            nodes.Previous.Next = nodes.Next;
            AddFirst(nodes.Value);
            return;
        }

        public void DeleteNode(T key)
        {
            Node temp = first, prev = null;
            
          
            if (temp != null && temp.Value.CompareTo(key) == 0)
            {
                first = temp.Next;
                return;
            }
            while (temp != null && temp.Value.CompareTo(key) != 0)
            {
                prev = temp;
                temp = temp.Next;
            }
            if (temp == null)
                return;
            prev.Next = temp.Next;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (last == null)
            {
                yield break;
            }
            Node tmp = last;
            while (tmp != null)
            {
                yield return tmp.Value;
                tmp = tmp.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Node
        {
            public T Value;
            public Node Next;
            public Node Previous;

            public Node(T value)
            {
                this.Value = value;
                Next = null;
                Previous = null;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}