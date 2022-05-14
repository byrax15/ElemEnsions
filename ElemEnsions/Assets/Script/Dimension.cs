using System;

namespace Script
{
    public enum Dimension
    {
        None,
        Fire,
        Water,
        Earth,
        Air,
    }

    public static class DimensionExtensions
    {
        public static bool ContainsDimensionName(this Dimension d, string s)
        {
            return s.Contains(d.ToString());
        }

        public static bool TryGetContainedDimension(this string s, out Dimension foundDimension)
        {
            var dimensions = Enum.GetValues(typeof(Dimension));
            foreach (var boxedDimension in dimensions)
            {
                var dimension = (Dimension) boxedDimension;
                var containsElement = dimension.ContainsDimensionName(s);

                if (containsElement)
                {
                    foundDimension = dimension;
                    return true;
                }
            }

            foundDimension = Dimension.None;
            return false;
        }
    }
}