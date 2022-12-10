using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [Header("Tablets")]
    [SerializeField] private GameObject sideTabletLed;
    [SerializeField] private GameObject frontTabletLed;

    [Header("LED")]
    [SerializeField] private Material ledOn;
    [SerializeField] private Material ledOff;

    [Header("Parameters")]
    [SerializeField] private float minDistance;
    [SerializeField] private float freqAdjust;
    [SerializeField] private float variationSpeedAdjust;

    [Header("Collectibles")]
    [SerializeField] private GameObject collectibleList;

    private float timer = 0;
    private bool ledActive = false;
    private AudioSource audiosource;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float closest = float.PositiveInfinity;

        for (int i = 0; i < collectibleList.transform.childCount; i++)
        {
            Transform child = collectibleList.transform.GetChild(i);

            float dist = Vector3.Distance(transform.position, child.position);
            if (dist < closest) closest = dist;
        }

        timer += freqAdjust * Mathf.Exp(-closest * variationSpeedAdjust) * Time.deltaTime;
        float state = Mathf.Sin(timer);

        if (closest < minDistance) ChangeLEDState(state > 0);
    }

    public void ChangeLEDState(bool active)
    {
        if (active == ledActive) return;

        audiosource.Play();
        MeshRenderer mesh;

        if (TabletController.tabletState == TabletController.TabletState.SIDE)
             mesh = sideTabletLed.GetComponent<MeshRenderer>();
        else mesh = frontTabletLed.GetComponent<MeshRenderer>();

        if (active) mesh.material = ledOn;
        else mesh.material = ledOff;

        ledActive = active;
    }
}
