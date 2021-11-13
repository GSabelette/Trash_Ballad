using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashpileController : MonoBehaviour
{   
    public GameObject model0;
    public GameObject model1;
    public GameObject model2;

    private List<GameObject> models = new List<GameObject>(); 
    private int modelNumber;
    private int totalModels = 3;
    // Start is called before the first frame update
    void Start()
    {
        modelNumber = 0;  
        models.Add(model0);
        models.Add(model1);
        models.Add(model2);

        models[0].setActive(true);
        for (var i = 1; i < totalModels; i++)
        {
            models[i].SetActive(false);
        }
    }

    void ModelSwitch() {
        for (var i = 0; i < totalModels; i++)
        {
            if (i != Collector.totalCollected)
            {   
                models[i].SetActive(false);
            }
            else {
                models[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Collect")) {
            ModelSwitch();
        }
    }
}
