using System;
using System.Linq;
using UnityEngine;

namespace Script
{
    [Serializable]
    public struct DimensionAffinity
    {
        public Dimension[] affinity;

        public bool HasAffinityWith(Dimension d) => affinity?.Contains(d) ?? false;
    }
}