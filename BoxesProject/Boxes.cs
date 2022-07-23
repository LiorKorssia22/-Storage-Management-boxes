using _1060DS;
using LinkedListAssignment;
using System;

namespace BoxesProject
{
    public class Boxes
    {
        public class BoxBase : IComparable<BoxBase>
        {

            public double Width { get; }
            public BST<BoxHeight> BstHeight { get; set; }

            public BoxBase(double buttomSize)
            {
                Width = buttomSize;
                BstHeight = new BST<BoxHeight>();
            }

            public int CompareTo(BoxBase other) => Width.CompareTo(other.Width);

            public override string ToString() => $"Width: {Width}\n";
        }

        public class BoxHeight : IComparable<BoxHeight>
        {

            public double Height { get; }
            public int Amount { get; set; }

            public DoubleLinkedList<TimeData>.Node NodeBox { get; set; }

            public TimeData TimeData { get; set; }
            public BoxHeight(double height, int amount)
            {
                Height = height;
                Amount = amount;
            }

            public int CompareTo(BoxHeight other) => Height.CompareTo(other.Height);
            public override string ToString() => $"\theight:  {Height}\tamount: {Amount} Time:{TimeData}\n";
        }
    }
}
