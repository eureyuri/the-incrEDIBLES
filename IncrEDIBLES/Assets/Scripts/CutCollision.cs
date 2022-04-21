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
    public Material cooked;

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
            else if(collided.CompareTag("knife"))
            {
                if(cut_num.enabled == true)
                {
                    int current = int.Parse(cut_num.text);
                    if(current == 1)
                    {
                        cut_num.text = "3";
                        cut_num.enabled = false;
                        GetComponent<XRGrabInteractable>().enabled = true;
                        var render = GetComponent<Renderer>();
                        render.material = cooked;
                        transform.parent = null;
                    }
                    else
                    {
                        cut_num.text = (current - 1).ToString();
                    }
                }
            }
        } 
        
    }
}
