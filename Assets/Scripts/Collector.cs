using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static int totalCollected = 0;

    private void OnTriggerStay(Collider other)
    {
        Collectible collectible = other.gameObject.GetComponent<Collectible>();
        if (collectible != null && !collectible.isCollected())
        {
            if (Input.GetButtonDown("Collect"))
            {
                collectible.SetCollected();
                CollectibleDataManager dataManager = other.gameObject.GetComponent<CollectibleDataManager>();
                CollectibleData data = dataManager.GetData();
                print(data.name);
                IncrementCount();

            }
        }
    }

    private static void IncrementCount()
    {
        totalCollected++;
    }
}
