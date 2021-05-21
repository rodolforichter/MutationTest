using System;

namespace NET5
{
    internal class CompareString : IComparable<string>
    {
        public int CompareTo(string other)
        {
            if(other == "249")
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}