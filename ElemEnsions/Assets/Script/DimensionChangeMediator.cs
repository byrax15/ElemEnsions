using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Script
{
    public class DimensionChangeMediator : MonoBehaviour
    {
        [SerializeField]
        private DimensionChangeEvent dimensionChanged = new();

        private Dimension _activeDimension;

        private void Start()
        {
            ChangeDimension(Dimension.None);
        }

        public void ChangeDimension(Dimension newDimension)
        {
            if (_activeDimension == newDimension)
                return;

            dimensionChanged.Invoke(_activeDimension, newDimension);
            _activeDimension = newDimension;
        }

        public void DebugChangeDimension(InputAction.CallbackContext callback)
        {
            if (callback.started)
            {
                if (!callback.action.name.TryGetContainedDimension(out var dimension))
                    return;

                ChangeDimension(dimension == _activeDimension
                    ? Dimension.None
                    : dimension
                );
            }
        }

        public void AddListener(UnityAction<Dimension,Dimension> callback)
        {
            dimensionChanged.AddListener(callback);
        }

        public void RemoveListener(UnityAction<Dimension,Dimension> callback)
        {
            dimensionChanged.RemoveListener(callback);
        }
    }
}