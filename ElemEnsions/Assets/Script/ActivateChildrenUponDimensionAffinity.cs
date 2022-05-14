using System.Threading.Tasks;
using UnityEngine;

namespace Script
{
    public class ActivateChildrenUponDimensionAffinity : MonoBehaviour
    {
        [SerializeField]
        private DimensionAffinity affinity = new()
        {
            affinity = new[] {Dimension.Base,},
        };

        [SerializeField] private DimensionChangeMediator dimensionChangeSource;

        private void Start()
        {
            if (dimensionChangeSource != null)
                dimensionChangeSource.AddListener(EnableChildrenIfHasAffinity);
        }

        private void EnableChildrenIfHasAffinity(Dimension oldDimension, Dimension newDimension)
        {
            var hasAffinity = affinity.HasAffinityWith(newDimension);
            Parallel.For(0, transform.childCount, childIndex =>
            {
                transform.GetChild(childIndex).gameObject.SetActive(hasAffinity);
            });
        }
    }
}