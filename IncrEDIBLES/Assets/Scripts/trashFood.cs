using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashFood : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("OverCooked"))
        {
            Destroy(collision.gameObject);
        }

    }
}
