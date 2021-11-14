using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSounds : MonoBehaviour
{
    [SerializeField] PlayerStepManager stepManager;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        // Subscribe the OnPlayerStep function to the event
        stepManager.OnPlayerStep += OnPlayerStep;
    }

    void OnPlayerStep()
    {
        Debug.Log(this.name + ": Step");
        audio.Play();
    }
}
