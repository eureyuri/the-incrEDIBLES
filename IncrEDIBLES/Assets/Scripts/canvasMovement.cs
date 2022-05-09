using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;
using UnityEngine.UI;

public class canvasMovement : MonoBehaviour
{
    private Vector3 relativePos;
    public GameObject cameraOffset;
    private Vector3 fakeForward ;
    private InputDevice targetDevice;
    private float distanceFactor = 0.761f;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }   
    }
    private void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        if(primaryButtonValue)
        { 
            adjustCanvas();
        }
        
    }
    private void adjustCanvas()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        relativePos = cameraOffset.transform.position;
        if(transform.localEulerAngles.y<=45 || transform.localEulerAngles.y>315)
        {
            relativePos += new Vector3(0.0f,0.0f,distanceFactor);
        }
        else if(transform.localEulerAngles.y<=135)
        {
            relativePos += new Vector3(distanceFactor,0.0f,0.0f);
        }
        else if(transform.localEulerAngles.y<=225)
        {
            relativePos += new Vector3(0.0f,0.0f,-distanceFactor);
        }
        else 
        {
            relativePos += new Vector3(-distanceFactor,0.0f,0.0f);
        }
        fakeForward = Camera.main.transform.forward;
        fakeForward.y = 0.0f; 
        fakeForward.Normalize(); 
        transform.position = Vector3.Lerp(transform.position, relativePos + fakeForward, Time.deltaTime+1.0f); 
    }
}

