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
    public static int curInventoryIndex;
    
    private void SwitchTabletState()
    {
        if (tabletState == TabletState.FRONT)
        {
            tabletState = TabletState.SIDE;
            tabletModelFront.SetActive(false);
            tabletModelSide.SetActive(true);
            LocalTabletManager.staticShipImage.enabled = false;
            tabletModelFront.GetComponent<LocalTabletManager>().clearInventoryDisplay();
        }
        else
        {
            // Set Tablet State and change Active Tablet Model
            tabletState = TabletState.FRONT;
            tabletModelSide.SetActive(false);
            tabletModelFront.SetActive(true);

            // The Tablet starts with LOGS status
            tabletFrontState = TabletFrontState.LOGS;
            // Change to corresponding Sprite and Display Log Text
            tabletModelFront.GetComponent<LocalTabletManager>().changeSprite(tabletFrontState);
            tabletModelFront.GetComponent<LocalTabletManager>().changeLogText(curLogIndex);
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
        // Change corresponding Tablet Front Sprite
        tabletModelFront.GetComponent<LocalTabletManager>().changeSprite(tabletFrontState);

        // If switching to Logs : Change log text
        if (tabletFrontState == TabletFrontState.LOGS)
        {
            tabletModelFront.GetComponent<LocalTabletManager>().changeLogText(curLogIndex);
        }

        // If switching to Ship : Enable ship Image | else Disable ship Image
        if (tabletFrontState == TabletFrontState.SHIP)
        {
            LocalTabletManager.staticShipImage.enabled = true;
        }
        else
        {
            LocalTabletManager.staticShipImage.enabled = false;
        }

        // If switching to Inventory
        if (tabletFrontState == TabletFrontState.TRASH)
        {
            tabletModelFront.GetComponent<LocalTabletManager>().changeInventoryDisplay(curInventoryIndex);
        }
        else
        {
            tabletModelFront.GetComponent<LocalTabletManager>().clearInventoryDisplay();
        }
    }

    private void SwitchCurLogIndex(string direction)
    {
        int maxIndex = LocalTabletManager.collectibleDataList.Count;
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

    private void SwitchCurInventoryIndex(string direction)
    {
        int maxIndex = LocalTabletManager.collectibleDataList.Count;
        if (maxIndex != 0)
        {
            if (direction.Equals("up"))
            {
                if (curInventoryIndex == 0)
                {
                    curInventoryIndex = maxIndex - 1;
                }
                else
                {
                    curInventoryIndex--;
                }
            }

            if (direction.Equals("down"))
            {
                if (curInventoryIndex == maxIndex - 1)
                {
                    curInventoryIndex = 0;
                }
                else
                {
                    curInventoryIndex++;
                }
            }
        }
        tabletModelFront.GetComponent<LocalTabletManager>().changeInventoryDisplay(curInventoryIndex);
    }
    // Start is called before the first frame update
    void Start()
    {
        tabletState = TabletState.SIDE;
        tabletFrontState = TabletFrontState.LOGS;
        tabletModelFront.SetActive(false);
        tabletModelSide.SetActive(true);
        curLogIndex = 0;
        curInventoryIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tablet"))
        {
            SwitchTabletState();
        }

        // Possible actions from Tablet Front Status
        if (tabletState == TabletState.FRONT)
        {   
            // Change columns
            if (Input.GetKeyDown("d"))
            {
                if (tabletFrontState == TabletFrontState.LOGS)
                {
                    tabletModelFront.GetComponent<LocalTabletManager>().clearLogText();
                }
                SwitchTabletFrontState("right");
            }
            if (Input.GetKeyDown("q"))
            {
                if (tabletFrontState == TabletFrontState.LOGS)
                {
                    tabletModelFront.GetComponent<LocalTabletManager>().clearLogText();
                }
                SwitchTabletFrontState("left");
            }

            // Possible actions from Tablet Front Log Status
            if (tabletFrontState == TabletFrontState.LOGS)
            {
                if (Input.GetKeyDown("s"))
                {
                    SwitchCurLogIndex("down");
                }
                if (Input.GetKeyDown("z"))
                {
                    SwitchCurLogIndex("up");
                }
            }
            // Possible actions from Tablet Front Inventory Status
            if (tabletFrontState == TabletFrontState.TRASH)
            {
                if (Input.GetKeyDown("s"))
                {
                    SwitchCurInventoryIndex("down");
                }
                if (Input.GetKeyDown("z"))
                {
                    SwitchCurInventoryIndex("up");
                }
            }
        }
    }
}
