using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CutCollision : MonoBehaviour
{
    public Text cut_num;
    public Text cut_num1;
    public Text cut_num2;

    // Start is called before the first frame update
    void Start()
    {
        // cut_num.enabled = false;
        // cut_num1.enabled = false;
        // cut_num2.enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if(collided.transform.childCount == 0)
        {
            if(collided.CompareTag("cut"))
            {
                cut_num.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collided.transform;
                transform.localPosition = new Vector3(0,0,0.0025f);
            }
            else if(collided.CompareTag("cut1"))
            {
                cut_num1.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collided.transform;
                transform.localPosition = new Vector3(0,0,0.0025f);
            }
            else if(collided.CompareTag("cut2"))
            {
                cut_num2.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collided.transform;
                transform.localPosition = new Vector3(0,0,0.0025f);
            }
        } 
        
    }
}
