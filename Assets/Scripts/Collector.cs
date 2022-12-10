using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            
            foreach (var (contact, collectible) in from contact in contactList.Where(contact => contact.gameObject.CompareTag("Collectible"))
                                                   let collectible = contact.gameObject.GetComponent<Collectible>()
                                                   where !collectible.isCollected()
                                                   select (contact, collectible))
            {
                collectible.SetCollected();
                CollectibleDataManager dataManager = contact.gameObject.GetComponent<CollectibleDataManager>();
                CollectibleData data = dataManager.GetData();
                
                audiosource.Play();
                
                LocalTabletManager.Instance.collectibleDataList.Add(data);
                LocalTabletManager.Instance.ReorderLogList();
                
                if (data.rocketElement)
                {
                    Collect();
                    LocalTabletManager.Instance.ChangeShipSprite();
                    TrashpileController.ModelAdd();
                }

                contact.gameObject.SetActive(false);
            }
        }
    }

    private static void Collect() => totalCollected++;
}
