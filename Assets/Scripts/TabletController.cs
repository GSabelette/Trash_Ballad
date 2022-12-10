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

    [SerializeField] private GameObject tabletModelSide;
    [SerializeField] private GameObject tabletModelFront;

    public static TabletState tabletState;
    public static TabletFrontState tabletFrontState;

    public static int curLogIndex;
    public static int curInventoryIndex;

    private AudioSource audio;
    private LocalTabletManager tabletManager;


    // Start is called before the first frame update
    void Start()
    {
        tabletState = TabletState.SIDE;
        tabletFrontState = TabletFrontState.LOGS;
        tabletModelFront.SetActive(false);
        tabletModelSide.SetActive(true);
        curLogIndex = 0;
        curInventoryIndex = 0;

        audio = GetComponent<AudioSource>();
        tabletManager = GetComponent<LocalTabletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Tablet"))
        {
            SwitchTabletState();
            audio.Play();
        }

        // Possible actions from Tablet Front Status
        if (tabletState == TabletState.FRONT)
        {
            // Change columns
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (tabletFrontState == TabletFrontState.LOGS)
                {
                    tabletManager.ClearLogText();
                }
                SwitchTabletFrontState("right");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (tabletFrontState == TabletFrontState.LOGS)
                {
                    tabletManager.ClearLogText();
                }
                SwitchTabletFrontState("left");
            }

            // Possible actions from Tablet Front Log Status
            if (tabletFrontState == TabletFrontState.LOGS)
            {
                if (Input.GetKeyDown(KeyCode.S)) SwitchCurLogIndex("down");
                if (Input.GetKeyDown(KeyCode.Z)) SwitchCurLogIndex("up");
            }
            // Possible actions from Tablet Front Inventory Status
            if (tabletFrontState == TabletFrontState.TRASH)
            {
                if (Input.GetKeyDown(KeyCode.S)) SwitchCurInventoryIndex("down");
                if (Input.GetKeyDown(KeyCode.Z)) SwitchCurInventoryIndex("up");
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

            LocalTabletManager.staticShipImage.enabled = false;
            tabletManager.clearInventoryDisplay();
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
        if (tabletFrontState == TabletFrontState.SHIP) 
             LocalTabletManager.staticShipImage.enabled = true;
        else LocalTabletManager.staticShipImage.enabled = false;

        // If switching to Inventory
        if (tabletFrontState == TabletFrontState.TRASH)
             tabletManager.ChangeInventoryDisplay(curInventoryIndex);
        else tabletManager.clearInventoryDisplay();
    }

    private void SwitchCurLogIndex(string direction)
    {
        int maxIndex = LocalTabletManager.collectibleDataList.Count;
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

    private void SwitchCurInventoryIndex(string direction)
    {
        int maxIndex = LocalTabletManager.collectibleDataList.Count;
        if (maxIndex != 0)
        {
            if (direction.Equals("up"))
            {
                if (curInventoryIndex == 0) curInventoryIndex = maxIndex - 1;
                else curInventoryIndex--;
            }

            if (direction.Equals("down"))
            {
                if (curInventoryIndex == maxIndex - 1) curInventoryIndex = 0;
                else curInventoryIndex++;
            }
        }

        tabletManager.ChangeInventoryDisplay(curInventoryIndex);
    }
}
