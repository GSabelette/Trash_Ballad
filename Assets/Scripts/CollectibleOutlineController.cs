using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleOutlineController : MonoBehaviour
{
    private Outline outline;
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outline.enabled = false;
        }
    }
}
