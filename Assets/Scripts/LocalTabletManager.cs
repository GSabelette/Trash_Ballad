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
        if (collectibleDataList.Count != 0)
        {
            logText.text = collectibleDataList[curLogIndex].year.ToString() + " : " + collectibleDataList[curLogIndex].description;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
