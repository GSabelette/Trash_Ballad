using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
public class PlayerStepManager : MonoBehaviour
{
    PlayerController player;

    public float stepSpacing = 3f;
    public float currentStepAmount = 0f;

    //public UnityEvent OnPlayerStep;
    
    public delegate void PlayerStepHandler();
    public event PlayerStepHandler OnPlayerStep;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if(player.onGround)
        {
            currentStepAmount += player.velocity.magnitude * Time.deltaTime;
            if (currentStepAmount >= stepSpacing)
            {
                // Step
                OnPlayerStep.Invoke();
                currentStepAmount -= stepSpacing;
            }
        }
    }
}
