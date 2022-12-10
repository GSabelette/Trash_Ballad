using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static int totalCollected = 0;
    private SphereCollider coll;
    private AudioSource audiosource;
    

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Collect") && TabletCollector.tabletteRecovered)
        {
            Collider[] contactList = Physics.OverlapSphere(coll.bounds.center, coll.radius);

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

                        audiosource.Play();

                        LocalTabletManager.collectibleDataList.Add(data);
                        LocalTabletManager.ReorderLogList();

                        if (data.rocketElement)
                        {
                            totalCollected++;
                            LocalTabletManager.ChangeShipSprite();
                            TrashpileController.ModelAdd();
                        }

                        contact.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
