using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(DimensionAffinity))]
    public class ActivateChildrenUponDimensionAffinity : MonoBehaviour
    {
        [SerializeField] private DimensionChangeMediator dimensionChangeSource;
        private DimensionAffinity _affinity;

        private void Start()
        {
            if (dimensionChangeSource != null)
                dimensionChangeSource.AddListener(EnableChildrenIfHasAffinity);
            
            if (!TryGetComponent(out _affinity))
                throw new NullReferenceException(nameof(_affinity));
        }

        private void EnableChildrenIfHasAffinity(Dimension oldDimension, Dimension newDimension)
        {
            var hasAffinity = _affinity.HasAffinityWith(newDimension);
            for (var childIndex = 0; childIndex < transform.childCount; childIndex++)
            {
                transform.GetChild(childIndex).gameObject.SetActive(hasAffinity);
            }
        }
    }
}