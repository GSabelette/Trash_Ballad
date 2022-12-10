using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStepSounds : MonoBehaviour
{
    [SerializeField] PlayerStepManager stepManager;

    [SerializeField] [Range(0f, 2f)] float minPitch = 0.8f;
    [SerializeField] [Range(0f, 2f)] float maxPitch = 1.4f;

    [SerializeField] AudioClip[] materialSounds;

    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        // Subscribe the OnPlayerStep function to the event
        stepManager.OnPlayerStep += OnPlayerStep;
    }

    void OnPlayerStep()
    {
        audiosource.pitch = Random.Range(minPitch, maxPitch);
        if (materialSounds.Length > 0) audiosource.clip = materialSounds[Random.Range(0, materialSounds.Length)];
        audiosource.Play();
    }
}
