using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static int totalCollected = 0;
    private SphereCollider collider;
    

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Collect"))
        {
            Collider[] contactList = Physics.OverlapSphere(collider.bounds.center, collider.radius);

            foreach (var contact in contactList)
            {
                if (contact.gameObject.CompareTag("Collectible"))
                {
                    Collectible collectible = contact.gameObject.GetComponent<Collectible>();
                    if (!collectible.isCollected())
                    {
                        collectible.SetCollected();
                        CollectibleDataManager dataManager = contact.gameObject.GetComponent<CollectibleDataManager>();
                        CollectibleData data = dataManager.GetData();

                        
                        LocalTabletManager.collectibleDataList.Add(data);
                        LocalTabletManager.reorderLogList();

                        if (data.rocketElement)
                        {
                            IncrementCount();
                            TrashpileController.ModelAdd();
                        }

                        contact.gameObject.SetActive(false);
                    }

                }
            }
        }
    }


    private static void IncrementCount()
    {
        totalCollected++;
    }
}
