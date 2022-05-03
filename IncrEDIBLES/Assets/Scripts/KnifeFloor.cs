using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFloor : MonoBehaviour
{
    Vector3 pos;
    Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if(collided.CompareTag("Floor"))
        {
            transform.position = pos;
            transform.rotation = rot;
        }
    }
}
