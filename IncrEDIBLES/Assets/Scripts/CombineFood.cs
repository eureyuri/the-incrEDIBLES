using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombineFood : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision) {
        GameObject collided = collision.gameObject;
        Debug.Log("CombinedFood: start");

        if (collided.CompareTag("ReadyPasta")) {
            Debug.Log("CombinedFood: collided");
            collided.GetComponent<XRGrabInteractable>().enabled = false;
            collided.GetComponent<Rigidbody>().isKinematic = true;
            collided.transform.parent = transform;
            collided.transform.localPosition = new Vector3(0, 0.05f, 0);
        } else {
            Debug.Log("CombinedFood: didnt collide");
        }
    }
}
