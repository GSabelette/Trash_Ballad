using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CollectibleData data = other.gameObject.GetComponent<CollectibleData>();
        if (data != null && !data.isCollected())
        {
            data.SetCollected();
            print("collected");
        }
    }
}
