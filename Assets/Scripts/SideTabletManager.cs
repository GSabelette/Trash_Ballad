using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideTabletManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nbLogText;
    [SerializeField] private GameObject collectibles;
    [SerializeField] private LocalTabletManager tabletManager;

    private int nbCollectibles;

    void Start()
    {
        nbLogText.text = "No items found";
        nbCollectibles = collectibles.transform.childCount;
    }

    void Update()
    {
        if (tabletManager.CollectiblesData.Count != 0)
        {
            nbLogText.text = "Items acquired : " + tabletManager.CollectiblesData.Count + " / " + nbCollectibles;
        }
    }
}
