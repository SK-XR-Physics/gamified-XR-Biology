using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biology2DCamera : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float smoothedSpeed = 0.125f;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 desiredPos = _target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothedSpeed);
        transform.position = smoothedPos;
    }
}