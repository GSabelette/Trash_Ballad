using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool collected = false;

    public bool IsCollected()
    {
        return collected;
    }

    public void SetCollected(bool state = true)
    {
        collected = state;
    }
}
