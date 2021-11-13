using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleData : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Texture2D picture;

    private bool collected = false;

    public bool isCollected()
    {
        return collected;
    }

    public void SetCollected(bool state = true)
    {
        collected = state;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDesc()
    {
        return description;
    }

    public Texture2D GetPicture()
    {
        return picture;
    }
}
