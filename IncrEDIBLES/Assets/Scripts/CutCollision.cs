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

    public GameObject cooked;
    Quaternion rotate;
    float z_axis;

    void Start()
    {
        rotate = transform.rotation;
        if (transform.position.y > 2.0f)
        {
            z_axis = 0.0025f;
        }
        else
        {
            z_axis = 0.0005f;
        }
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
                transform.localPosition = new Vector3(0,0,z_axis);
                transform.rotation = rotate;  
            }
            else if(collided.CompareTag("cut1"))
            {
                cut_num1.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collided.transform;
                transform.localPosition = new Vector3(0,0,z_axis);
                transform.rotation = rotate;
                //transform.localPosition = new Vector3(0,0,0.0025f);
            }
            else if(collided.CompareTag("cut2"))
            {
                cut_num2.enabled = true;
                GetComponent<XRGrabInteractable>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collided.transform;
                transform.localPosition = new Vector3(0,0,z_axis);
                transform.rotation = rotate;
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
                        Vector3 pos = transform.position;
                        Quaternion rot = transform.rotation;
                        Destroy(gameObject);
                        GameObject clone = Instantiate(cooked, pos, rot);
                        clone.GetComponent<XRGrabInteractable>().enabled = true;
                    }
                    else
                    {
                        cut_num.text = (current - 1).ToString();
                    }
                }
            }
            else if(collided.CompareTag("knife1"))
            {
                if(cut_num1.enabled == true)
                {
                    int current = int.Parse(cut_num1.text);
                    if(current == 1)
                    {
                        cut_num1.text = "3";
                        cut_num1.enabled = false;
                        Vector3 pos = transform.position;
                        Quaternion rot = transform.rotation;
                        Destroy(gameObject);
                        GameObject clone = Instantiate(cooked, pos, rot);
                        clone.GetComponent<XRGrabInteractable>().enabled = true;
                    }
                    else
                    {
                        cut_num1.text = (current - 1).ToString();
                    }
                }
            }
            else if(collided.CompareTag("knife2"))
            {
                if(cut_num2.enabled == true)
                {
                    int current = int.Parse(cut_num2.text);
                    if(current == 1)
                    {
                        cut_num2.text = "3";
                        cut_num2.enabled = false;
                        Vector3 pos = transform.position;
                        Quaternion rot = transform.rotation;
                        Destroy(gameObject);
                        GameObject clone = Instantiate(cooked, pos, rot);
                        clone.GetComponent<XRGrabInteractable>().enabled = true;
                    }
                    else
                    {
                        cut_num2.text = (current - 1).ToString();
                    }
                }
            }
        } 
        
    }
}
