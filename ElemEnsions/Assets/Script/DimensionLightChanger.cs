using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace Script
{
    public class DimensionLightChanger : MonoBehaviour
    {
        public DimensionChangeMediator mediator;
        private Light _light;

        private void Start()
        {
            mediator.AddListener(ChangeLight);
            _light = GetComponent<Light>();
        }

        public void ChangeLight(Dimension oldDimension, Dimension newDimension)
        {
            _light.color = DimensionColor.ColorFor(newDimension);
        }
    }
}