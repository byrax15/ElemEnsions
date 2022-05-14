using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.FLAMMABLE_TAG))
        {
            print("burn!!!");
            StartCoroutine(Burn(other.gameObject));
        }
    }

    private static IEnumerator Burn(GameObject flammableObject)
    {
        yield return new WaitForSeconds(3);
        Destroy(flammableObject);
    }
}
