using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{

    [SerializeField] PlayerStepManager stepManager;
    [SerializeField] private float wobbleHeight;

    private Vector3 basePos;

    // Start is called before the first frame update
    void Start()
    {
        basePos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float step_value = stepManager.currentStepAmount / stepManager.stepSpacing;
        transform.localPosition = basePos + new Vector3(basePos.x, -Mathf.Cos(step_value * 2 * Mathf.PI) * wobbleHeight, basePos.z);
        stepManager.currentStepAmount = Mathf.Lerp(stepManager.currentStepAmount, stepManager.stepSpacing / 2f, Time.deltaTime);
    }
}
