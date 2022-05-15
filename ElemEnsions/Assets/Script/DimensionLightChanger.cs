using UnityEngine;
using System.Collections;

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
            StartCoroutine(ChangeLightDelay(newDimension));
        }

        private IEnumerator ChangeLightDelay(Dimension newDimension)
        {
            yield return new WaitForSeconds(0.3f);
            _light.color = DimensionColor.ColorFor(newDimension);
        }
    }
}