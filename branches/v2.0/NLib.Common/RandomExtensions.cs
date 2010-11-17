using System;
using System.Collections.Generic;
using System.Text;

namespace NLib
{
    public static class RandomExtensions
    {
        //--- Public Static Methods ---

        public static bool NextBool(this Random random)
        {
            bool result = false;
            int next = random.Next(2);
            if (next != 0)
                result = true;
            return result;
        }
    }
}
