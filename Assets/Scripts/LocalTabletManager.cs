using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class LocalTabletManager : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI logText;
    [SerializeField] public Sprite spriteLogs;
    [SerializeField] public Sprite spriteTrash;
    [SerializeField] public Sprite spriteShip;
    Dictionary<TabletController.TabletFrontState, Sprite> tabletFrontStateMap = new Dictionary<TabletController.TabletFrontState, Sprite>();
    public static List<CollectibleData> collectibleDataList = new List<CollectibleData>();
    // Start is called before the first frame update
    void Start()
    {
        tabletFrontStateMap[TabletController.TabletFrontState.LOGS] = spriteLogs;
        tabletFrontStateMap[TabletController.TabletFrontState.TRASH] = spriteTrash;
        tabletFrontStateMap[TabletController.TabletFrontState.SHIP] = spriteShip;
    }

    public void changeSprite(TabletController.TabletFrontState tabletFrontState) 
    {
        image.sprite = tabletFrontStateMap[tabletFrontState];
    }

    public static void reorderLogList()
    {
        collectibleDataList.Sort((data1, data2) => data1.year.CompareTo(data2.year));
    }

    public void changeLogText(int curLogIndex)
    {
        string totalString = "";
        if (collectibleDataList.Count != 0)
        {
            for (int i = 0; i < Collector.totalCollected; i++)
            {
                if (i != curLogIndex)
                {
                    totalString += "Log " + i.ToString() + " | year " + collectibleDataList[i].year + "\n";
                }
                if (i == curLogIndex)
                {
                    totalString += "Log " + i.ToString() + " | year " + collectibleDataList[i].year + " : " + collectibleDataList[i].description + "\n";
                }
                
            }
            logText.text = totalString;
        }
    }

    public void clearLogText()
    {
        logText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if ( Collector.totalCollected == 1 && Input.GetButtonDown("Tablet"))
        {
            changeLogText(0);
        }
    }
}
