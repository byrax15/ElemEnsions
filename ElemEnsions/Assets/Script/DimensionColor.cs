using System.Collections;
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
            dict.Add(Dimension.Base, Color.black);
            dict.Add(Dimension.Fire, new Color(0.97f, 0.35f, 0.09f));
            dict.Add(Dimension.Water, new Color(0.19f, 0.4f, 0.9f));
            dict.Add(Dimension.Earth, new Color(0.66f, 0.86f, 0.15f));
            dict.Add(Dimension.Air, new Color(0.92f, 0.14f, 0.76f));
            return dict;
        }

        public static IEnumerable<KeyValuePair<Dimension, Color>> Values => ColorDictionary;
    }
}