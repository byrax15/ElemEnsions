using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    [RequireComponent(typeof(DimensionAffinity))]
    [ExecuteAlways]
    public class DimensionChanger : MonoBehaviour
    {
        [SerializeField] private DimensionChangeMediator dimensionMediator;
        
        private ParticleSystem.MainModule _particle;
        private Light _light;
        private DimensionAffinity _affinity;

        private void Start()
        {
            StartVfx();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                dimensionMediator.ChangeDimension(_affinity.FirstNotBaseOrDefault);
        }

        private void StartVfx()
        {
            _affinity = GetComponent<DimensionAffinity>();
            _particle = GetComponentInChildren<ParticleSystem>().main;
            _light = GetComponentInChildren<Light>();

            var affinityColor = _affinity.AffinityColor();
            _particle.startColor = affinityColor;
            _light.color = affinityColor;
        }
    }
}