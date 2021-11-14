using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SideTabletManager : MonoBehaviour
{
    public TextMeshProUGUI nbLogText;
    // Start is called before the first frame update
    void Start()
    {
        nbLogText.text = "No Logs Found";
    }

    // Update is called once per frame
    void Update()
    {
        if (LocalTabletManager.collectibleDataList.Count != 0)
        {
            nbLogText.text = "Total Logs Acquired : " + LocalTabletManager.collectibleDataList.Count;
        }
    }
}
