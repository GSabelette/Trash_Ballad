using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private SphereCollider collider;


    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float closest = float.PositiveInfinity;

        Collider[] contactList = Physics.OverlapSphere(collider.bounds.center, collider.radius);

        foreach (var contact in contactList)
        {
            if (contact.gameObject.CompareTag("Collectible"))
            {
                float dist = Vector3.Distance(transform.position, contact.transform.position);
                if ( dist < closest)
                {
                    closest = dist;
                }
            }
        }

        print(closest);
    }
}
