using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothCameraTime;
    
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 offset;

    void Awake()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothCameraTime);
    }

}