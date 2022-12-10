using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCollector : MonoBehaviour
{
    [SerializeField] private GameObject playerTablet;
    [SerializeField] private GameObject groundTablet;

    private SphereCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<SphereCollider>();
        playerTablet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Collect"))
        {
            Collider[] contactList = Physics.OverlapSphere(coll.bounds.center, coll.radius);

            foreach (var contact in contactList)
            {
                if (contact.gameObject.CompareTag("Player"))
                {
                    groundTablet.SetActive(false);
                    playerTablet.SetActive(true);
                }
            }
        }
    }
}
