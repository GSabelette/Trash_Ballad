using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashpileController : MonoBehaviour
{

    [SerializeField] private List<GameObject> modelList;

    public static List<GameObject> models = new List<GameObject>(); 
    //private int modelNumber;
    public static int totalModels;


    private SphereCollider coll;


    // Start is called before the first frame update
    void Start()
    {
        initData(modelList);
        coll = GetComponent<SphereCollider>();
    }

    private static void initData(List<GameObject> modelListLocal)
    {
        totalModels = modelListLocal.Count;

        for (var i = 0; i < totalModels; i++)
        {
            models.Add(modelListLocal[i]);
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
            for (int i = 0; i < totalModels - 1; i++)
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
        if (Input.GetButtonDown("Collect") && Collector.totalCollected == totalModels)
        {
            Collider[] contactList = Physics.OverlapSphere(coll.bounds.center, coll.radius);

            foreach (var contact in contactList)
            {
                if (contact.gameObject.CompareTag("Player"))
                {
                    SceneManager.LoadScene("Credits");
                }
            }
        }
    }
}
