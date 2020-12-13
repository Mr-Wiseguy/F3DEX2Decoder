using System;
using System.Collections.Generic;
using System.Text;

namespace F3DEX2Decoder
{
    public static class Utils
    {
        public static int GetBitsAndShift(ref ulong source, int numBits)
        {
            int retVal = (int)(source & (ulong)((1 << numBits) - 1));
            source >>= numBits;
            return retVal;
        }
    }
}
