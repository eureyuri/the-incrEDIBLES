using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombineFood : MonoBehaviour
{
    private List<string> foodOnPlate;
    public GameObject tomatoPasta;
    public GameObject tomatoCheesePasta;
    public GameObject tomatoMeatPasta;
    public GameObject cheesePasta;
    public GameObject cheeseMeatPasta;
    public GameObject intermediateDish;

    void Start() {
        foodOnPlate = new List<string>();
    }

    public void OnCollisionEnter(Collision collision) {
        GameObject collided = collision.gameObject;

        if (foodOnPlate.Count == 0) {
            AddOne(collided);
        } else if (foodOnPlate.Count == 1) {
            CombineTwo(collided);
        } else if (foodOnPlate.Count == 2) {
            CombineThree(collided);
        }
    }

    private void AddOne(GameObject collided) {
        if (IsValidFood(collided)) {
            collided.GetComponent<XRGrabInteractable>().enabled = false;
            collided.GetComponent<Rigidbody>().isKinematic = true;
            collided.transform.parent = this.transform;
            collided.transform.localPosition = new Vector3(0, 0.1f, 0);
            AddToPlate(collided);
        }
    }

    private bool IsValidFood(GameObject collided) {
        return (collided.CompareTag("ReadyPasta") || collided.CompareTag("ReadyTomato") || collided.CompareTag("ReadyCheese") || collided.CompareTag("ReadyMeat"));
    }

    private void AddToPlate(GameObject collided) {
        if (collided.CompareTag("ReadyPasta") && !foodOnPlate.Contains("pasta")) {
            Debug.Log("CombineFood: added pasta");
            foodOnPlate.Add("pasta");
        } else if (collided.CompareTag("ReadyTomato") && !foodOnPlate.Contains("tomato")) {
            foodOnPlate.Add("tomato");
        } else if (collided.CompareTag("ReadyFish") && !foodOnPlate.Contains("fish")) {
            foodOnPlate.Add("fish");
        } else if (collided.CompareTag("ReadyMushroom") && !foodOnPlate.Contains("mushroom")) {
            foodOnPlate.Add("mushroom");
        } else if (collided.CompareTag("ReadyCheese") && !foodOnPlate.Contains("cheese")) {
            Debug.Log("CombineFood: added cheese");
            foodOnPlate.Add("cheese");
        } else if (collided.CompareTag("ReadyMeat") && !foodOnPlate.Contains("meat")) {
            foodOnPlate.Add("meat");
        }
    }

    private void CombineTwo(GameObject collided) {
        if (IsValidFood(collided)) {
            Debug.Log("CombineFood: CombineTwo");
            AddToPlate(collided);

            if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato")) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(collided);
                GameObject childObject = Instantiate(tomatoPasta) as GameObject;
                childObject.transform.parent = transform;
                childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese")) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(collided);
                // FIXME: not correctly set as child of plate
                GameObject childObject = Instantiate(cheesePasta) as GameObject;
                childObject.transform.parent = transform;
                childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            }
            // else {
            //     GameObject childObject = Instantiate(intermediateDish) as GameObject;
            //     childObject.transform.parent = transform;
            //     childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            // }
        }
    }

    private void CombineThree(GameObject collided) {
        if (IsValidFood(collided)) {
            Debug.Log("CombineFood: CombineThree");
            AddToPlate(collided);

            if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("cheese")) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(collided);
                GameObject childObject = Instantiate(tomatoCheesePasta) as GameObject;
                childObject.transform.parent = transform;
                childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("meat")) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(collided);
                GameObject childObject = Instantiate(tomatoMeatPasta) as GameObject;
                childObject.transform.parent = transform;
                childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("meat")) {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(collided);
                GameObject childObject = Instantiate(cheeseMeatPasta) as GameObject;
                childObject.transform.parent = transform;
                childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
            }
        }
    }
}
