using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    [ExecuteAlways]
    public class DimensionAffinity : MonoBehaviour
    {
        public Dimension[] affinity = {Dimension.Base,};

        public bool HasAffinityWith(Dimension d) => affinity?.Contains(d) ?? false;

        private void OnDrawGizmos()
        {
            Gizmos.color = DimensionColor.ColorFor(affinity.FirstOrDefault(NotBaseDimension));
            Gizmos.DrawCube(transform.position + new Vector3(0, 1,0), new (.3f,.3f,.3f));
        }

        private static bool NotBaseDimension(Dimension dim) => dim is not Dimension.Base;
    }
}