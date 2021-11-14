using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private GameObject sideTabletLed;
    [SerializeField] private GameObject frontTabletLed;


    [SerializeField] private Material ledOn;
    [SerializeField] private Material ledOff;

    [SerializeField] private float freqAdjust;

    private SphereCollider collider;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float closest = collider.radius;

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

        timer += freqAdjust * (collider.radius - closest) * Time.deltaTime;

        float state = Mathf.Sin(timer);


        MeshRenderer mesh;

        if (TabletController.tabletState == TabletController.TabletState.SIDE)
        {
            mesh = sideTabletLed.GetComponent<MeshRenderer>();
        }
        else
        {
            mesh = frontTabletLed.GetComponent<MeshRenderer>();
        }

        if (state > 0)
        {
            mesh.material = ledOn;
        }
        else
        {
            mesh.material = ledOff;
        }
    }
}
