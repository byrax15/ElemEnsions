using System;
using UnityEngine;

namespace Script
{
    [RequireComponent(typeof(DimensionAffinity))]
    public class Traversable : Interactable
    {
        [SerializeField] private DimensionChangeMediator dimensionMediator;
        private DimensionAffinity _affinity;

        private void Start()
        {
            _affinity = GetComponent<DimensionAffinity>();
        }

        public override bool Interact() => ChangeDimension();

        private bool ChangeDimension()
        {
            dimensionMediator.ChangeDimension(_affinity.FirstNotBaseOrDefault);
            return true;
        }
    }
}