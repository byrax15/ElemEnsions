using System;
using System.Collections;
using UnityEngine;

namespace Script.Interactions
{
    public class Openable : Interactable
    {
        private Transform _cover;

        private void Start()
        {
            _cover = transform.GetChild(0).Find("Coffre_dessus");
        }

        public override bool Interact() => Open();

        private bool Open()
        {
            StartCoroutine(MoveCover());
            return true;
        }

        private IEnumerator MoveCover()
        {
            Debug.Log("YOU WON");
            if (_cover)
            {
                var destination = Quaternion.Euler(90, 0, 0);
                while (_cover.localRotation != destination)
                {
                    _cover.localRotation = Quaternion.Lerp(_cover.localRotation, destination, Time.fixedDeltaTime);
                    yield return new WaitForFixedUpdate();
                }
            }

        }
    }
}