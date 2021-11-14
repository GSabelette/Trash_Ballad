using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{

    [SerializeField] private float wobbleSpeed;
    [SerializeField] private float wobbleHeight;

    private Vector3 basePos;
    private Vector3 prevPos;


    private float dist = 0;

    // Start is called before the first frame update
    void Start()
    {
        basePos = transform.localPosition;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        prevPos.y = transform.position.y;
        dist += Vector3.Distance(transform.position, prevPos);
        transform.localPosition = basePos + new Vector3(0, wobbleHeight * Mathf.Sin(dist * wobbleSpeed), 0);
        prevPos = transform.position;
    }
}
