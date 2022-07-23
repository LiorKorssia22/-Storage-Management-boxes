using _1060DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public class BoxDataInfo
    {
        public Boxes.BoxBase BoxBase { get; set; }
        public Boxes.BoxHeight BoxHeight { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        internal BST<Boxes.BoxHeight> BoxHeights { get; private set; }

        internal bool Blank { get; private set; }

        public int Amount { get; set; }

        internal BoxDataInfo(Boxes.BoxBase boxBaseI, Boxes.BoxHeight boxHeightI, bool blank, int amount)
        {
            BoxBase = boxBaseI;
            BoxHeight = boxHeightI;
            Blank = blank;
            BoxHeights = boxBaseI.BstHeight;
            X = boxBaseI.Width;
            Y = boxHeightI.Height;
            Amount = amount;
        }
    }
}
