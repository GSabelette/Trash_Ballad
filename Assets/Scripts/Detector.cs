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
    [SerializeField] private float variationSpeedAdjust;

    [SerializeField] private GameObject colletibleList;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float closest = float.PositiveInfinity;

        for (int i = 0; i < colletibleList.transform.childCount; i++)
        {
            Transform child = colletibleList.transform.GetChild(i);

            float dist = Vector3.Distance(transform.position, child.position);
            if (dist < closest)
            {
                closest = dist;
            }
        }


        timer += freqAdjust * Mathf.Exp(-closest * variationSpeedAdjust) * Time.deltaTime;

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
