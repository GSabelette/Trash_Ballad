using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private GameObject tabletModelSide;
    [SerializeField] private GameObject tabletModelFront;

    public static TabletState tabletState;
    public static TabletFrontState tabletFrontState;

    private int curLogIndex;
    private int curItemIndex;

    private AudioSource audiosource;
    private LocalTabletManager tabletManager;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        tabletManager = GetComponentInChildren<LocalTabletManager>();
    }

    void Start()
    {
        tabletState = TabletState.SIDE;
        tabletFrontState = TabletFrontState.LOGS;
        tabletModelFront.SetActive(false);
        tabletModelSide.SetActive(true);

        curLogIndex = 0;
        curItemIndex = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Tablet"))
        {
            SwitchTabletState();
            audiosource.Play();
        }

        // Possible actions from Tablet Front Status
        if (tabletState == TabletState.FRONT)
        {
            // Change columns
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (tabletFrontState == TabletFrontState.LOGS) tabletManager.ClearLogText();
                SwitchTabletFrontState("right");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (tabletFrontState == TabletFrontState.LOGS) tabletManager.ClearLogText();
                SwitchTabletFrontState("left");
            }

            // Possible actions from Tablet Front Log Status
            if (tabletFrontState == TabletFrontState.LOGS)
            {
                if (Input.GetKeyDown(KeyCode.S)) UpdateLogIndex("down");
                if (Input.GetKeyDown(KeyCode.Z)) UpdateLogIndex("up");
            }

            // Possible actions from Tablet Front Inventory Status
            if (tabletFrontState == TabletFrontState.TRASH)
            {
                if (Input.GetKeyDown(KeyCode.S)) UpdateInventoryIndex("down");
                if (Input.GetKeyDown(KeyCode.Z)) UpdateInventoryIndex("up");
            }
        }
    }

    private void SwitchTabletState()
    {
        if (tabletState == TabletState.FRONT)
        {
            tabletState = TabletState.SIDE;

            tabletModelFront.SetActive(false);
            tabletModelSide.SetActive(true);

            tabletManager.ShowShip(false);
            tabletManager.ClearInventoryDisplay();
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
            tabletManager.ChangeSprite(tabletFrontState);
            tabletManager.ChangeLogText(curLogIndex);
        }
    }

    private void SwitchTabletFrontState(string direction)
    {   
        if (direction.Equals("right"))
        {
            if (tabletFrontState == TabletFrontState.SHIP) tabletFrontState = TabletFrontState.LOGS;
            else tabletFrontState++;
        }
        if (direction.Equals("left"))
        {
            if (tabletFrontState == TabletFrontState.LOGS) tabletFrontState = TabletFrontState.SHIP;
            else tabletFrontState--;
        }

        // Change corresponding Tablet Front Sprite
        tabletManager.ChangeSprite(tabletFrontState);

        // If switching to Logs : Change log text
        if (tabletFrontState == TabletFrontState.LOGS) tabletManager.ChangeLogText(curLogIndex);

        // If switching to Ship : Enable ship Image | else Disable ship Image
        if (tabletFrontState == TabletFrontState.SHIP) tabletManager.ShowShip(true);
        else tabletManager.ShowShip(false);

        // If switching to Inventory
        if (tabletFrontState == TabletFrontState.TRASH)
             curItemIndex = tabletManager.UpdateInventoryDisplay(curItemIndex, 0);
        else tabletManager.ClearInventoryDisplay();
    }

    private void UpdateLogIndex(string direction)
    {
        int maxIndex = tabletManager.CollectiblesData.Count;
        if (maxIndex != 0)
        {
            if (direction.Equals("up"))
            {
                if (curLogIndex == 0) curLogIndex = maxIndex - 1;
                else curLogIndex--;
            }

            if (direction.Equals("down"))
            {
                if (curLogIndex == maxIndex -1) curLogIndex = 0;
                else curLogIndex++;
            }
        }

        tabletManager.ChangeLogText(curLogIndex);
    }

    private void UpdateInventoryIndex(string direction)
    {
        if (direction.Equals("up")) curItemIndex = tabletManager.UpdateInventoryDisplay(curItemIndex, -1);
        if (direction.Equals("down")) curItemIndex = tabletManager.UpdateInventoryDisplay(curItemIndex, 1);        
    }
}
