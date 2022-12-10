using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
public class LocalTabletManager : MonoBehaviour
{
    private int LINE_LENGTH = 37;
    private int INVENTORY_LINE_LENGTH = 32;

    // Logs
    public Image image;
    public TextMeshProUGUI logText;

    // Tablet Front Map Generals
    [SerializeField] public Sprite spriteLogs;
    [SerializeField] public Sprite spriteTrash;
    [SerializeField] public Sprite spriteShip;
    Dictionary<TabletController.TabletFrontState, Sprite> tabletFrontStateMap = new Dictionary<TabletController.TabletFrontState, Sprite>();
    
    // Inventory
    public static List<CollectibleData> collectibleDataList = new List<CollectibleData>();

    // Ship 
    [SerializeField] public Image shipImage;
    public static Image staticShipImage;
    [SerializeField] public List<Sprite> shipSpriteList;
    public static List<Sprite> staticShipSpriteList;

    // Inventory
    public Image inventoryImage;
    public TextMeshProUGUI inventoryText;

    // Start is called before the first frame update
    void Start()
    {
        tabletFrontStateMap[TabletController.TabletFrontState.LOGS] = spriteLogs;
        tabletFrontStateMap[TabletController.TabletFrontState.TRASH] = spriteTrash;
        tabletFrontStateMap[TabletController.TabletFrontState.SHIP] = spriteShip;

        staticShipImage = shipImage;
        staticShipSpriteList = shipSpriteList;
        shipImage.enabled = false;
        staticShipImage.enabled = false;
    }

    void Update()
    {
        if (collectibleDataList.Count == 1 && Input.GetButtonDown("Tablet"))
        {
            ChangeLogText(0);
        }
    }

    public void ChangeSprite(TabletController.TabletFrontState tabletFrontState) 
    {
        image.sprite = tabletFrontStateMap[tabletFrontState];
    }

    public static void ReorderLogList()
    {
        collectibleDataList.Sort((data1, data2) => data1.year.CompareTo(data2.year));
    }

    private String PartialDisplayLog(int logIndex)
    {
        CollectibleData curData = collectibleDataList[logIndex];
        return ("Log " + (logIndex).ToString() + " | year " + curData.year + "\n");
    }

    private String FullDisplayLog(int curLogIndex)
    {
        // Middle log
        // All the 17 below are = LINE_LENGTH - 20
        String localString = "";
        CollectibleData curData = collectibleDataList[curLogIndex];
        int nbLines = (curData.description.Length + 20) / LINE_LENGTH;
        localString += "Log " + curLogIndex.ToString() + " | year " + curData.year + " : " + curData.description.Substring(0, LINE_LENGTH - 20) + "\n";

        for (int j = 1; j < nbLines; j++)
        {
            localString += curData.description.Substring(17 + (j - 1) * LINE_LENGTH, LINE_LENGTH) + "\n";
        }

        localString += curData.description.Substring(17 + (nbLines - 1) * LINE_LENGTH, curData.description.Length - (17 + (nbLines - 1) * LINE_LENGTH)) + "\n";
        return localString;
    }

    public void ChangeLogText(int curLogIndex)
    {
        string totalString = "";

        if (collectibleDataList.Count != 0)
        {
            // Limit case lower bound
            if (curLogIndex == 0)
            {
                totalString += FullDisplayLog(curLogIndex);
                if (collectibleDataList.Count >= 2)
                {
                    totalString += PartialDisplayLog(curLogIndex + 1);
                }
            }
            // Limit case upper bound
            else if (curLogIndex == collectibleDataList.Count - 1) 
            {
                if (collectibleDataList.Count >= 2)
                {
                    totalString += PartialDisplayLog(curLogIndex - 1);
                }
                totalString += FullDisplayLog(curLogIndex);
            }
            // Normal case
            else
            {
                // Upper log
                totalString += PartialDisplayLog(curLogIndex - 1);
                // Main log
                totalString += FullDisplayLog(curLogIndex);
                // Lower log
                totalString += PartialDisplayLog(curLogIndex + 1);
            }

            logText.text = totalString;
        }
    }

    public void ClearLogText()
    {
        logText.text = "";
    }

    public static void ChangeShipSprite()
    {
        print("Changing staticShipImage sprite");
        staticShipImage.sprite = staticShipSpriteList[Collector.totalCollected];
    }
    
    public void ChangeInventoryDisplay(int curInventoryIndex)
    {
        inventoryImage.enabled = true;
        inventoryImage.sprite = collectibleDataList[curInventoryIndex].picture;
        String description = collectibleDataList[curInventoryIndex].description;
        String totalString = "";
        int nbLines = description.Length / INVENTORY_LINE_LENGTH + 1;
        for (int j = 1; j < nbLines; j++)
        {
            totalString += description.Substring((j - 1) * INVENTORY_LINE_LENGTH, INVENTORY_LINE_LENGTH) + "\n";
        }
        totalString += description.Substring((nbLines - 1) * INVENTORY_LINE_LENGTH, description.Length - (nbLines - 1) * INVENTORY_LINE_LENGTH);

        inventoryText.text = totalString;
    }

    public void clearInventoryDisplay()
    {
        inventoryImage.enabled = false;
        inventoryText.text = "";
    }
}
