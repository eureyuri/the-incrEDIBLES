using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombineFood : MonoBehaviour
{
    private List<string> foodOnPlate;
    public GameObject tomatoPasta;

    void Start() {
        foodOnPlate = new List<string>();
    }

    public void OnCollisionEnter(Collision collision) {
        GameObject collided = collision.gameObject;

        if (foodOnPlate.Count == 0) {
            AddOne(collided);
        } else if (foodOnPlate.Count == 1) {
            CombineTwo(collided);
        }
    }

    private void AddOne(GameObject collided) {
        collided.GetComponent<XRGrabInteractable>().enabled = false;
        collided.GetComponent<Rigidbody>().isKinematic = true;
        collided.transform.parent = this.transform;
        collided.transform.localPosition = new Vector3(0, 0.1f, 0);
        AddToPlate(collided);
    }

    private void AddToPlate(GameObject collided) {
        if (collided.CompareTag("ReadyPasta")) {
            Debug.Log("CombineFood: add pasta");
            if (!foodOnPlate.Contains("pasta")) foodOnPlate.Add("pasta");
        } else if (collided.CompareTag("ReadyTomato")) {
            Debug.Log("CombineFood: add tomato");
            if (!foodOnPlate.Contains("tomato")) foodOnPlate.Add("tomato");
        }
    }

    private void CombineTwo(GameObject collided) {
        Debug.Log("CombineFood: AddTwo");
        AddToPlate(collided);

        Debug.Log("CombineFood: contains?: " + (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato")));
        if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato")) {
            Destroy(transform.GetChild(0).gameObject);
            Destroy(collided);
            GameObject childObject = Instantiate(tomatoPasta) as GameObject;
            childObject.transform.parent = transform;
            childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
        }
    }
}
