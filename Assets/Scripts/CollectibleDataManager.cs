using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollectibleData
{
    public bool rocketElement;
    public bool item;
    public string name;
    public string description;
    public int year;
    public Sprite picture;
}


public class CollectibleDataManager : MonoBehaviour
{
    [SerializeField] private bool rocketElement;
    [SerializeField] private bool item;
    [SerializeField] private string title;
    [SerializeField] private string description;
    [SerializeField] private int year;
    [SerializeField] private Sprite picture;


    public CollectibleData GetData()
    {
        CollectibleData data = new CollectibleData
        {
            rocketElement = rocketElement,
            item = item,
            name = title,
            description = description,
            year = year,
            picture = picture
        };

        return data;
    }
}
