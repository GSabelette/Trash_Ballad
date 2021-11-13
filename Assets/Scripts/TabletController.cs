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

    public enum TabletFrontState
    {
        LOGS,
        INVENTORY,
        SHIP
    };  

    public static TabletState tabletState;
    public static TabletFrontState tabletFrontState;
    [SerializeField] private GameObject tabletModelSide;
    [SerializeField] private GameObject tabletModelFront;

    private void SwitchTabletState()
    {
        if (tabletState == TabletState.FRONT)
        {
            tabletState = TabletState.SIDE;
            tabletModelFront.SetActive(false);
            tabletModelSide.SetActive(true);
            tabletFrontState = TabletFrontState.LOGS;
        }
        else
        {
            tabletState = TabletState.FRONT;
            tabletModelSide.SetActive(false);
            tabletModelFront.SetActive(true);
        }
    }

    private void SwitchTabletFrontState(string direction)
    {   
        if (direction.Equals("right"))
        {
            if (tabletFrontState == TabletFrontState.LOGS || tabletFrontState == TabletFrontState.INVENTORY)
            {
                tabletFrontState++;
            }
            else
            {
                tabletFrontState = TabletFrontState.LOGS;
            }
        }
        if (direction.Equals("left"))
        {
            if (tabletFrontState == TabletFrontState.INVENTORY || tabletFrontState == TabletFrontState.SHIP)
            {
                tabletFrontState--;
            }
            else
            {
                tabletFrontState = TabletFrontState.SHIP;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        tabletState = TabletState.SIDE;
        tabletFrontState = TabletFrontState.LOGS;
        tabletModelFront.SetActive(false);
        tabletModelSide.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tablet"))
        {
            SwitchTabletState();
        }
        if (tabletState == TabletState.FRONT)
        {
            if (Input.GetKeyDown("d"))
            {
                SwitchTabletFrontState("right");
            }
            if (Input.GetKeyDown("q"))
            {
                SwitchTabletFrontState("left");
            }
        }
    }
}
