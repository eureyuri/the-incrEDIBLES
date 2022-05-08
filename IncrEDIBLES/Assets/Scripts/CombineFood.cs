using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CombineFood : MonoBehaviour
{
    private List<string> foodOnPlate;
    public GameObject tomatoPasta;
    public GameObject tomatoCheesePasta;
    public GameObject tomatoMushroomPasta;
    public GameObject tomatoMeatPasta;
    public GameObject tomatoFishPasta;
    public GameObject tomatoMushroomMeatPasta;
    public GameObject tomatoMushroomFishPasta;
    public GameObject cheesePasta;
    public GameObject cheeseMushroomPasta;
    public GameObject cheeseMeatPasta;
    public GameObject cheeseFishPasta;
    public GameObject cheeseMushroomFishPasta;
    public GameObject cheeseMushroomMeatPasta;
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
        } else if (foodOnPlate.Count == 3) {
            CombineFour(collided);
        }
    }

    private void AddOne(GameObject collided) {
        if (IsValidFood(collided)) {
            collided.GetComponent<OffsetGrabInteractable>().enabled = false;
            collided.GetComponent<Rigidbody>().isKinematic = true;
            collided.transform.parent = this.transform;
            collided.transform.localPosition = new Vector3(0, 0.1f, 0);
            AddToPlate(collided);
        }
    }

    private bool IsValidFood(GameObject collided) {
        return (collided.CompareTag("ReadyPasta") || collided.CompareTag("ReadyTomato") || collided.CompareTag("ReadyCheese") || collided.CompareTag("ReadyMeat") || collided.CompareTag("ReadyMushroom") || collided.CompareTag("ReadyFish"));
    }

    private void AddToPlate(GameObject collided) {
        if (collided.CompareTag("ReadyPasta") && !foodOnPlate.Contains("pasta")) {
            foodOnPlate.Add("pasta");
        } else if (collided.CompareTag("ReadyTomato") && !foodOnPlate.Contains("tomato")) {
            foodOnPlate.Add("tomato");
        } else if (collided.CompareTag("ReadyFish") && !foodOnPlate.Contains("fish")) {
            foodOnPlate.Add("fish");
        } else if (collided.CompareTag("ReadyMushroom") && !foodOnPlate.Contains("mushroom")) {
            foodOnPlate.Add("mushroom");
        } else if (collided.CompareTag("ReadyCheese") && !foodOnPlate.Contains("cheese")) {
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
                InstantiateDish(tomatoPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese")) {
                InstantiateDish(cheesePasta, collided);
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
                InstantiateDish(tomatoCheesePasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("mushroom")) {
                InstantiateDish(tomatoMushroomPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("fish")) {
                InstantiateDish(tomatoFishPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("meat")) {
                InstantiateDish(tomatoMeatPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("meat")) {
                InstantiateDish(cheeseMeatPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("mushroom")) {
                InstantiateDish(cheeseMushroomPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("fish")) {
                InstantiateDish(cheeseFishPasta, collided);
            }
        }
    }

    private void CombineFour(GameObject collided) {
        if (IsValidFood(collided)) {
            Debug.Log("CombineFood: CombineFour");
            AddToPlate(collided);

            if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("mushroom") && foodOnPlate.Contains("meat")) {
                InstantiateDish(tomatoMushroomMeatPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("tomato") && foodOnPlate.Contains("mushroom") && foodOnPlate.Contains("fish")) {
                InstantiateDish(tomatoMushroomFishPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("mushroom") && foodOnPlate.Contains("meat")) {
                InstantiateDish(cheeseMushroomMeatPasta, collided);
            } else if (foodOnPlate.Contains("pasta") && foodOnPlate.Contains("cheese") && foodOnPlate.Contains("mushroom") && foodOnPlate.Contains("fish")) {
                InstantiateDish(cheeseMushroomFishPasta, collided);
            }
        }
    }

    private void InstantiateDish(GameObject dish, GameObject collided) {
        for (int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        Destroy(collided);
        GameObject childObject = Instantiate(dish) as GameObject;
        childObject.transform.parent = transform;
        childObject.transform.localPosition = new Vector3(0, 0.1f, 0);
    }

}
