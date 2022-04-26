using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform cameraTransofrm;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        cameraTransofrm = Camera.main.transform;
        lastCameraPosition = cameraTransofrm.position;
    }

    private void Update()
    {
        var deltaMovement = cameraTransofrm.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = cameraTransofrm.position;
    }
}
