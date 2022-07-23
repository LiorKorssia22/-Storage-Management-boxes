using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BoxesProject.Boxes;

namespace BoxesProject
{
    public class TimeData : IComparable<TimeData>, IEnumerable<TimeData>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public DateTime LastTimeSold { get; set; }
        public BoxHeight BoxHeightNodeProp { get; set; }
        public BoxBase BoxBaseNodeProp { get; set; }

        public TimeData(  double x, double y)
        {
            X= x;
            Y = y;
            LastTimeSold = DateTime.Now;
        }

        public int CompareTo(TimeData other) => (this.Y.CompareTo(other.Y));

        public IEnumerator<TimeData> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return $"\t{LastTimeSold}\n";
        }
    }
}
