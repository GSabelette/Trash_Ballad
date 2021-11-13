using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CollectibleData
{
    public string name;
    public string description;
    public Texture2D picture;
}


public class CollectibleDataManager : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Texture2D picture;


    public CollectibleData GetData()
    {
        CollectibleData data = new CollectibleData();
        data.name = name;
        data.description = description;
        data.picture = picture;
        return data;
    }
}
