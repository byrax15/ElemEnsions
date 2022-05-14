using System.Collections.Generic;
using UnityEngine;

namespace Script
{
    public static class DimensionColor
    {
        private static readonly IDictionary<Dimension, Color> ColorDictionary = DictInitializer();

        public static Color ColorFor(Dimension d) => ColorDictionary[d];

        private static IDictionary<Dimension, Color> DictInitializer()
        {
            var dict = new Dictionary<Dimension, Color>();
            dict.Add(Dimension.Base, new Color(0.29f, 0.29f, 0.29f));
            dict.Add(Dimension.Fire, Color.red);
            dict.Add(Dimension.Water, Color.blue);
            dict.Add(Dimension.Earth, Color.green);
            dict.Add(Dimension.Air, Color.white);
            return dict;
        }
    }
}