using System;
using UnityEngine.Events;

namespace Script
{
    [Serializable]
    public class DimensionChangeEvent : UnityEvent<Dimension, Dimension>
    {
    }
}