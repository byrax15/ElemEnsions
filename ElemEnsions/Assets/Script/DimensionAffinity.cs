using System;
using System.Linq;
using UnityEngine;

namespace Script
{
    [ExecuteInEditMode]
    public class DimensionAffinity : MonoBehaviour
    {
        public Dimension[] affinity = {Dimension.Base,};

        public bool HasAffinityWith(Dimension d) => affinity?.Contains(d) ?? false;
    }
}