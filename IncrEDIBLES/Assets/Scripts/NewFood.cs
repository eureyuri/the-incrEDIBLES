using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NewFood : MonoBehaviour
{
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

    public void initFood()
    {
        if(first)
        {
            GameObject clone = Instantiate(gameObject, pos, rot);
            clone.GetComponent<XRGrabInteractable>().enabled = true;
            first = false;
        }
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
