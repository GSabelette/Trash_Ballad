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
            print("detected");
            if (Input.GetButtonDown("Collect"))
            {
                print("collect");
                collectible.SetCollected();
                CollectibleDataManager dataManager = other.gameObject.GetComponent<CollectibleDataManager>();
                CollectibleData data = dataManager.GetData();
                print(data.name);
                IncrementCount();
                LocalTabletManager.collectibleDataList.Add(data);
                LocalTabletManager.reorderLogList();

            }
        }
    }

    private static void IncrementCount()
    {
        totalCollected++;
    }
}
