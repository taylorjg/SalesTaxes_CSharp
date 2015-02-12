using System;

namespace Code
{
    public static class DecimalExtensions
    {
        public static decimal RoundUp(this decimal n)
        {
            return Math.Ceiling(n * 20m) / 20.0m;
        }
    }
}
