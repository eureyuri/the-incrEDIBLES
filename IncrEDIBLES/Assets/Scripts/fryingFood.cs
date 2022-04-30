using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class fryingFood : MonoBehaviour
{
    [SerializeField] public Image redFill;
    [SerializeField] public Image greenFill;
    public GameObject overcookTimer;
    public GameObject cookTimer;
    private int Duration = 60;
    private int remainingDuration;
    private Vector3 position;
    private Quaternion rotation;
    private GameObject collisionObject;
    private GameObject newPrefab;
    public GameObject steakPrefab;
    public GameObject steakOvercookedPrefab;
    public GameObject cookedTomatoPrefab;
    public GameObject overCookedTomatoPrefab;

    void Start()
    {
        cookTimer.SetActive(false);
        overcookTimer.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ingredient put in frying pan");
        collisionObject = collision.gameObject;
        position = collisionObject.transform.position;
        rotation = collisionObject.transform.rotation;
        if (collision.gameObject.CompareTag("Steak") || collision.gameObject.CompareTag("CutTomato"))
        {
            Debug.Log("Food in frying pan");
            cookTimer.SetActive(true);
            collisionObject.GetComponent<XRGrabInteractable>().enabled = false;
            if (collision.gameObject.CompareTag("Steak"))
            {
                Begin(Duration, greenFill, "steak");
            }
            else if(collision.gameObject.CompareTag("CutTomato"))
            {
                Begin(Duration, greenFill, "tomato");
            }
            
        }
    }

     void OnCollisionExit(Collision collision)
    {
        if((collision.gameObject.CompareTag("ReadyMeat") || collision.gameObject.CompareTag("ReadyTomato")) && overcookTimer.activeSelf)
        {
            Debug.Log("Food taken out from frying pan");
            overcookTimer.SetActive(false);
        }
    }

    private void Begin(int Second, Image uiFill, string food)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer(uiFill, food));
    }
    
    private IEnumerator UpdateTimer(Image uiFill, string food)
    {
        while(remainingDuration>0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            Debug.Log("timer -1");
            yield return new WaitForSeconds(0.2f);
            if(remainingDuration==0)
            {
                Destroy(collisionObject);
                yield return new WaitForSeconds(0.2f);
                OnEnd(food);
            }
        }
        
    }
    
    private void OnEnd(string food)
    {
        if (food.Equals("steak"))
        {
            if(cookTimer.activeSelf)
            {
                collisionObject = Instantiate(steakPrefab, position, rotation);
                cookTimer.SetActive(false);
                overcookTimer.SetActive(true);  
                Begin(Duration, redFill, food);
            }
            else if(overcookTimer.activeSelf)
            {
                Debug.Log("inside overcooked steak");
                overcookTimer.SetActive(false);
                collisionObject = Instantiate(steakOvercookedPrefab, position, rotation);
                Debug.Log("overcooked steak created");
            }
        }
        else if (food.Equals("tomato"))
        {
            if(cookTimer.activeSelf)
            {
                collisionObject = Instantiate(cookedTomatoPrefab, position, rotation);
                cookTimer.SetActive(false);
                overcookTimer.SetActive(true);  
                Begin(Duration, redFill, food);
            }
            else if(overcookTimer.activeSelf)
            {
                overcookTimer.SetActive(false);
                collisionObject = Instantiate(overCookedTomatoPrefab, position, rotation);
            }
        }
        else{
            Debug.Log(food);
        }
    }
}
