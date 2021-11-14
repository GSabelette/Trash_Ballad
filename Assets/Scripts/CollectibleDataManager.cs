using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct CollectibleData
{
    public bool rocketElement;
    public string name;
    public string description;
    public int year;
    public Sprite picture;
}


public class CollectibleDataManager : MonoBehaviour
{
    [SerializeField] private bool rocketElement;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private int year;
    [SerializeField] private Sprite picture;


    public CollectibleData GetData()
    {
        CollectibleData data = new CollectibleData();
        data.rocketElement = rocketElement;
        data.name = name;
        data.description = description;
        data.year = year;
        data.picture = picture;
        return data;
    }
}
