using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    public class DimensionChangeMediator : MonoBehaviour
    {
        [CanBeNull] public Dimension defaultDimension;
        
        public UnityEvent<Dimension, Dimension> dimensionChangeRequested = new();
        public UnityEvent<Dimension, Dimension> dimensionChanged = new();
        
        private Dimension _activeDimension;

        private void Start()
        {
            if (defaultDimension == null)
                _activeDimension = AssetDatabase.LoadAssetAtPath<Dimension>("Assets/ScriptableObjectInstances/Dimensions/None.asset");
            if (_activeDimension == null) throw new NullReferenceException(nameof(_activeDimension));
        }

        public void ChangeDimension(Dimension newDimension)
        {
            if (_activeDimension.GetType() == newDimension.GetType()) 
                return;
            
            dimensionChanged.Invoke(_activeDimension, newDimension);
            _activeDimension = newDimension;
        }
    }
}
