using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Collector : MonoBehaviour
{
    [SerializeField] private Detector detector;
    [SerializeField] private LocalTabletManager tabletManager;
    [SerializeField] private TrashpileController trashpile;

    private SphereCollider coll;
    private AudioSource audiosource;

    private void Start()
    {
        coll = GetComponent<SphereCollider>();
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Collect"))
        {
            Collider[] contactList = Physics.OverlapSphere(coll.bounds.center, coll.radius);
            
            foreach (var (contact, collectible) in from contact in contactList.Where(contact => contact.gameObject.CompareTag("Collectible"))
                                                   let collectible = contact.gameObject.GetComponent<Collectible>()
                                                   where !collectible.IsCollected()
                                                   select (contact, collectible))
            {
                collectible.SetCollected();
                CollectibleDataManager dataManager = contact.gameObject.GetComponent<CollectibleDataManager>();
                CollectibleData data = dataManager.GetData();
                
                audiosource.Play();
                
                tabletManager.CollectiblesData.Add(data);
                tabletManager.ReorderLogList();

                if (data.rocketElement)
                {
                    tabletManager.CollectShipPart();
                    trashpile.AddModel();
                }
                else if (data.item) tabletManager.CollectItem(data.picture);

                Destroy(contact.gameObject);
                detector.ChangeLEDState(false);
            }
        }
    }
}
