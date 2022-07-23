using _1060DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    internal class Box
    {
        internal class BoxBase : IComparable<BoxBase>
        {
            public BST<Box.BoxHeight> BstHeight { get; set; }
            double _x;

            LinkedList<BoxHeight> heightCollection;

            public BoxBase(double buttomSize)
            {
                _x = buttomSize;
            }

            public int CompareTo(BoxBase other) => _x.CompareTo(other._x);
        }
        internal class BoxHeight : IComparable<BoxHeight>
        {
            double _height;
            public double Height { get; set; }

            int _amount;
            public int Amount { get => _amount; set => value = _amount; }

            DateTime _lastPurchaseDate;

            public BoxHeight(double height, int amount)
            {
                _height = height;
                _amount = amount;
            }

            public int CompareTo(BoxHeight other)
            {
                return _height.CompareTo(other._height);
            }
        }
    }


}
