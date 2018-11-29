﻿using UnityEngine;
using System.Collections;

public class TranslateToCube : MonoBehaviour
{
    public FreeTrackClientDll fTInput;
    public bool useRAWData;
    public Transform target;
    public Camera cam;

    public float positionScale = 0.001f;
    public float positionLerpRate = 5;
    public float rotationScale = 60.0f;
    public float rotationLerpRate = 5;

    public float FOVscale;

    private Vector3 startPos = Vector3.zero;
    private Vector3 addedPos = Vector3.zero;

    private Vector3 startEul = Vector3.zero;
    private Vector3 addedEul = Vector3.zero;

    private float startFOV;

    void Start()
    {
        startPos = target.position;
        startEul = target.eulerAngles;
        startFOV = cam.fieldOfView;
    }

    void Update()
    {
        addedPos = Vector3.Lerp(addedPos, (new Vector3(fTInput.X, -fTInput.Y, -fTInput.Z * 7.5f) * positionScale), positionLerpRate * Time.deltaTime);
        addedEul = new Vector3(
           Mathf.LerpAngle(addedEul.x, ((useRAWData == true ? fTInput.RawY : fTInput.Y) / 500f) * rotationScale, rotationLerpRate * Time.deltaTime),
           Mathf.LerpAngle(addedEul.y, ((useRAWData == true ? fTInput.RawX : fTInput.X) / 500f) * rotationScale, rotationLerpRate * Time.deltaTime),
           // Mathf.LerpAngle(addedEul.y, (useRAWData == true ? fTInput.RawYaw : fTInput.Yaw) * rotationScale, rotationLerpRate * Time.deltaTime),
           0f
           );

        if (target)
        {
            target.position = startPos + addedPos;
            target.eulerAngles = startEul + addedEul;
            cam.fieldOfView = startFOV + (addedPos.z * FOVscale);
        }
    }
}
