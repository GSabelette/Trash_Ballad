using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        TRASH,
        SHIP
    };  

    public static TabletState tabletState;
    public static TabletFrontState tabletFrontState;
    [SerializeField] private GameObject tabletModelSide;
    [SerializeField] private GameObject tabletModelFront;
    public static int curLogIndex;
    
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
            if (tabletFrontState == TabletFrontState.LOGS || tabletFrontState == TabletFrontState.TRASH)
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
            if (tabletFrontState == TabletFrontState.TRASH || tabletFrontState == TabletFrontState.SHIP)
            {
                tabletFrontState--;
            }
            else
            {
                tabletFrontState = TabletFrontState.SHIP;
            }
        }
        tabletModelFront.GetComponent<LocalTabletManager>().changeSprite(tabletFrontState);
    }
    
    private void SwitchCurLogIndex(string direction)
    {
        int maxIndex = Collector.totalCollected;
        if (maxIndex != 0)
        {
            if (direction.Equals("up"))
            {
                if (curLogIndex == 0)
                {
                    curLogIndex = maxIndex - 1;
                }
                else
                {
                    curLogIndex--;
                }
            }

            if (direction.Equals("down"))
            {
                if (curLogIndex == maxIndex -1)
                {
                    curLogIndex = 0;
                }
                else
                {
                    curLogIndex++;
                }
            }
        }
        tabletModelFront.GetComponent<LocalTabletManager>().changeLogText(curLogIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        tabletState = TabletState.SIDE;
        tabletFrontState = TabletFrontState.LOGS;
        tabletModelFront.SetActive(false);
        tabletModelSide.SetActive(true);
        curLogIndex = 0;
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
