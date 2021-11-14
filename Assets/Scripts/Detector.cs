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

    private MeshRenderer sideRenderer;
    private MeshRenderer frontRenderer;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<SphereCollider>();
        sideRenderer = sideTabletLed.GetComponent<MeshRenderer>();
        frontRenderer = frontRenderer.GetComponent<MeshRenderer>();
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

        timer += freqAdjust * (1 / closest) * Time.deltaTime;

        float state = Mathf.Sin(timer);

        if (state > 0)
        {
            sideRenderer.material = ledOn;
            frontRenderer.material = ledOn;
        }
        else
        {
            sideRenderer.material = ledOff;
            frontRenderer.material = ledOff;
        }
    }
}
