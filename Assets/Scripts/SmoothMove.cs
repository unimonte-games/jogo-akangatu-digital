using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMove : MonoBehaviour
{
    public Vector3 targetPosition;
    public float velocity;
    Transform tr;

    void Awake()
    {
        tr = transform;
    }

    void Start ()
    {
        targetPosition = tr.localPosition;
    }

    void Update()
    {
        tr.localPosition = Vector3.Lerp(
            tr.localPosition,
            targetPosition,
            Time.deltaTime * velocity
        );
    }
}
