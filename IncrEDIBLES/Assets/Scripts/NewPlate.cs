using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NewPlate : MonoBehaviour
{
    public Image arrow;

    bool first;
    Vector3 pos;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        first = true;
        pos = transform.position;
        rot = transform.rotation;
    }

    public void ExitGrab()
    {
        if(first)
        {
            Debug.Log("Create a new plate");
            GameObject clone = Instantiate(gameObject, pos, rot);
            foreach (Transform child in clone.transform) 
            {
                if(child.CompareTag("ReadyPasta") || child.CompareTag("ReadyTomato") || child.CompareTag("ReadyCheese") || child.CompareTag("ReadyMeat") || child.CompareTag("ReadyMushroom") || child.CompareTag("ReadyFish"))
                {
                    Debug.Log("plate remove child");
                    GameObject.Destroy(child.gameObject);
                }
            }
            clone.GetComponent<OffsetGrabInteractable>().enabled = true;
            Debug.Log("plate offset");
            first = false;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        arrow.enabled = true;
    }

    public void StartGrab()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        arrow.enabled = false;
    }
}
