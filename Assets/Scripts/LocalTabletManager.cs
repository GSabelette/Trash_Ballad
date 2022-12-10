using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
public class LocalTabletManager : MonoBehaviour
{
    [Header("Logs")]
    public Image image;
    public TextMeshProUGUI logText;

    [Header("Tablet Front Map Generals")]
    [SerializeField] private Sprite spriteLogs;
    [SerializeField] private Sprite spriteTrash;
    [SerializeField] private Sprite spriteShip;

    private readonly Dictionary<TabletController.TabletFrontState, Sprite> tabletFrontStateMap = new Dictionary<TabletController.TabletFrontState, Sprite>();

    [Header("Ship")] 
    [SerializeField] private Image shipImage;
    [SerializeField] private List<Sprite> shipSpriteList;

    public static Image staticShipImage;
    public static List<Sprite> staticShipSpriteList;

    [Header("Inventory")]
    public Image inventoryImage;
    public TextMeshProUGUI inventoryText;

    public static List<CollectibleData> collectibleDataList = new List<CollectibleData>();

    // Start is called before the first frame update
    void Start()
    {
        tabletFrontStateMap.Add(TabletController.TabletFrontState.LOGS, spriteLogs);
        tabletFrontStateMap.Add(TabletController.TabletFrontState.TRASH, spriteTrash);
        tabletFrontStateMap.Add(TabletController.TabletFrontState.SHIP, spriteShip);

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
        tabletFrontStateMap.TryGetValue(tabletFrontState, out var sprite);
        if (sprite != null) image.sprite = sprite;
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

    private string FullDisplayLog(int curLogIndex)
    {
        CollectibleData curData = collectibleDataList[curLogIndex];
        return "Log " + curLogIndex.ToString() + " | year " + curData.year + " : " + curData.description + "\n";
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
        staticShipImage.sprite = staticShipSpriteList[Collector.totalCollected];
    }
    
    public void ChangeInventoryDisplay(int curInventoryIndex)
    {
        if (collectibleDataList.Count == 0) return;

        inventoryImage.enabled = true;
        inventoryImage.sprite = collectibleDataList[curInventoryIndex].picture;
        inventoryText.text = collectibleDataList[curInventoryIndex].description;
    }

    public void ClearInventoryDisplay()
    {
        inventoryImage.enabled = false;
        inventoryText.text = "";
    }
}
