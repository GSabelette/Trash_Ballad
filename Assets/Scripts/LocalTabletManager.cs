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

    [Header("Tablet backgrounds")]
    [SerializeField] private Sprite spriteLogs;
    [SerializeField] private Sprite spriteTrash;
    [SerializeField] private Sprite spriteShip;
    [SerializeField] private Sprite spriteSettings;

    private readonly Dictionary<TabletController.TabletFrontState, Sprite> tabletFrontStateMap = new Dictionary<TabletController.TabletFrontState, Sprite>();

    [Header("Ship")] 
    [SerializeField] private Image shipImage;
    [SerializeField] private TextMeshProUGUI shipText;
    [SerializeField] private List<Sprite> shipSpriteList;

    [Header("Inventory")]
    [SerializeField] private Image inventoryImage;
    [SerializeField] private TextMeshProUGUI inventoryText;
    [SerializeField] private GameObject inventoryContainer;

    [Header("Settings")]
    [SerializeField] private GameObject settingsCanvas;

    private Image[] inventoryIcons;

    public List<CollectibleData> CollectiblesData { get; private set; } = new List<CollectibleData>();
    public int ShipPartsCollected { get; private set; } = 0;
    private int itemsCollected = 0;

    void Awake()
    {
        tabletFrontStateMap.Add(TabletController.TabletFrontState.LOGS, spriteLogs);
        tabletFrontStateMap.Add(TabletController.TabletFrontState.TRASH, spriteTrash);
        tabletFrontStateMap.Add(TabletController.TabletFrontState.SHIP, spriteShip);
        tabletFrontStateMap.Add(TabletController.TabletFrontState.SETTINGS, spriteSettings);

        inventoryIcons = inventoryContainer.GetComponentsInChildren<Image>();

        ShowShip(false);
        DisplayInventory(false);
        DisplaySettings(false);
    }

    void Update()
    {
        if (CollectiblesData.Count == 1 && Input.GetButtonDown("Tablet"))
        {
            ChangeLogText(0);
        }
    }

    public void ChangeSprite(TabletController.TabletFrontState tabletFrontState) 
    {
        tabletFrontStateMap.TryGetValue(tabletFrontState, out var sprite);
        if (sprite != null) image.sprite = sprite;
    }

    public void ReorderLogList()
    {
        CollectiblesData.Sort((data1, data2) => data1.year.CompareTo(data2.year));
    }

    private String PartialDisplayLog(int logIndex)
    {
        CollectibleData curData = CollectiblesData[logIndex];
        return ("Log " + (logIndex).ToString() + " | year " + curData.year + "\n");
    }

    private string FullDisplayLog(int curLogIndex)
    {
        CollectibleData curData = CollectiblesData[curLogIndex];
        return "Log " + curLogIndex.ToString() + " | year " + curData.year + " : " + curData.description + "\n";
    }

    public void ChangeLogText(int curLogIndex)
    {
        string totalString = "";
        int nbCollectible = CollectiblesData.Count;

        if (nbCollectible == 0) return;

        if (curLogIndex > 0)
        {
            totalString += PartialDisplayLog(curLogIndex - 1);
            totalString += "\n";
        }

        totalString += FullDisplayLog(curLogIndex);

        if (curLogIndex + 1 < nbCollectible)
        {
            totalString += "\n";
            totalString += PartialDisplayLog(curLogIndex + 1);
        }

        logText.text = totalString;
    }

    public void ClearLogText() => logText.text = "";

    public void CollectItem(Sprite icon)
    {
        inventoryIcons[itemsCollected].sprite = icon;
        itemsCollected++;
    }

    public void CollectShipPart()
    {
        ShipPartsCollected++;
        UpdateShip();
    }

    public void ShowShip(bool active)
    {
        shipImage.enabled = active;
        shipText.enabled = active;
    }

    public void UpdateShip()
    {
        shipImage.sprite = shipSpriteList[ShipPartsCollected];
        shipText.text = "Ship parts : " + ShipPartsCollected + " / " + (shipSpriteList.Count - 1).ToString();
    }

    public void DisplayInventory(bool active)
    {
        inventoryContainer.SetActive(active);

        if(!active)
        {
            inventoryText.text = "";
            inventoryContainer.SetActive(false);
        }
    }

    public int UpdateInventoryDisplay(int curIndex, int change)
    {
        if (itemsCollected == 0) return curIndex;

        int index = ClampIndex(curIndex + change);
        if (change == 0) change = 1;

        while (!CollectiblesData[index].item) index = ClampIndex(index + change);  

        inventoryImage.enabled = true;
        inventoryImage.sprite = CollectiblesData[index].picture;
        inventoryText.text = CollectiblesData[index].description;

        return index;
    }

    private int ClampIndex(int index)
    {
        if (index < 0) index = CollectiblesData.Count - 1;
        if (index >= CollectiblesData.Count) index = 0;

        return index;
    }

    public void DisplaySettings(bool active) => settingsCanvas.SetActive(active);
}
