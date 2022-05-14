using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Script
{
    public class DimensionChangeMediator : MonoBehaviour
    {
        public UnityEvent<Dimension, Dimension> dimensionChanged = new();

        public Dimension _activeDimension;

        private void Start()
        {
            _activeDimension = Dimension.None;
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
                if (!callback.action.name.TryGetContainedDimensionName(out var dimension))
                    return;

                ChangeDimension(dimension == _activeDimension
                    ? Dimension.None
                    : dimension
                );

                Debug.Log(_activeDimension.ToString());
            }
        }
    }
}