using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletController : MonoBehaviour
{
    public enum TabletState
    {
        SIDE,
        FRONT
    };

    public static TabletState tabletState;
    private GameObject tabletModelSide;
    private GameObject tabletModelFront;

    private void SwitchTabletState()
    {
        if (tabletState == TabletState.FRONT)
        {
            tabletState = TabletState.SIDE;
            tabletModelFront.SetActive(false);
            tabletModelSide.SetActive(true);
        }
        else
        {
            tabletState = TabletState.FRONT;
            tabletModelSide.SetActive(false);
            tabletModelFront.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tabletState = TabletState.SIDE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tablet"))
        {
            SwitchTabletState();
        }
    }
}
