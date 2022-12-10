using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideTabletManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nbLogText;
    [SerializeField] private GameObject collectibles;

    private int nbCollectibles;

    void Start()
    {
        nbLogText.text = "No Logs Found";
        nbCollectibles = collectibles.transform.childCount;
    }

    void Update()
    {
        if (LocalTabletManager.Instance.collectibleDataList.Count != 0)
        {
            nbLogText.text = "Logs Acquired : " + LocalTabletManager.Instance.collectibleDataList.Count + " / " + nbCollectibles;
        }
    }
}
