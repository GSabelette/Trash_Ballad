using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashpileController : MonoBehaviour
{   
    public GameObject model0;
    public GameObject model1;
    public GameObject model2;

    public static List<GameObject> models = new List<GameObject>(); 
    //private int modelNumber;
    public static int totalModels = 3;
    // Start is called before the first frame update
    void Start()
    {
        //modelNumber = 0;  
        models.Add(model0);
        models.Add(model1);
        models.Add(model2);

        models[0].SetActive(true);
        for (var i = 1; i < totalModels; i++)
        {
            models[i].SetActive(false);
        }
    }

    public static void ModelAdd() {
        if (Collector.totalCollected != totalModels)
        {
            print("Added model " + Collector.totalCollected);
            models[Collector.totalCollected - 1].SetActive(true);
        }
        else
        {
            // totalModels - 2 because the last model is going to be used
            for (int i = 0; i < totalModels - 2; i++)
            {
                models[i].SetActive(false);
            }
            // Set the last model (spaceship) active
            models[totalModels - 1].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
