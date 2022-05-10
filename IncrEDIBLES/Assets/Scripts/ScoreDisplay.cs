using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreField;

    private bool once = true;
    private Vector3 relativePos;
    public GameObject cameraOffset;
    private Vector3 fakeForward;
    private float distanceFactor = 118f;
    // Start is called before the first frame update
    void Start()
    {
        
        scoreField.text = Score.score.ToString();
    }

    void Update()
    {
        if(once)
        {
            adjustCanvas();
            once = false;
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
