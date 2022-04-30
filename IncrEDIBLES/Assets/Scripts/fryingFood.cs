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
    public GameObject cookedFishPrefab;
    public GameObject overCookedFishPrefab;
    public GameObject cookedMushroomPrefab;
    public GameObject overCookedMushroomPrefab;

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
        if (collision.gameObject.CompareTag("Steak") || collision.gameObject.CompareTag("CutTomato") || collision.gameObject.CompareTag("Fish") || collision.gameObject.CompareTag("CutMushroom"))
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
            else if(collision.gameObject.CompareTag("Fish"))
            {
                Begin(Duration, greenFill, "fish");
            }
            else if(collision.gameObject.CompareTag("CutMushroom"))
            {
                Begin(Duration, greenFill, "mushroom");
            }
        }
    }

     void OnCollisionExit(Collision collision)
    {
        if((collision.gameObject.CompareTag("ReadyMeat") || collision.gameObject.CompareTag("ReadyTomato") || collision.gameObject.CompareTag("ReadyFish") || collision.gameObject.CompareTag("ReadyMushroom")) && overcookTimer.activeSelf)
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
            if(remainingDuration==0 && (overcookTimer.activeSelf || cookTimer.activeSelf))
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
        else if (food.Equals("fish"))
        {
            if(cookTimer.activeSelf)
            {
                collisionObject = Instantiate(cookedFishPrefab, position, rotation);
                cookTimer.SetActive(false);
                overcookTimer.SetActive(true);  
                Begin(Duration, redFill, food);
            }
            else if(overcookTimer.activeSelf)
            {
                overcookTimer.SetActive(false);
                collisionObject = Instantiate(overCookedFishPrefab, position, rotation);
            }
        }
        else if (food.Equals("mushroom"))
        {
            if(cookTimer.activeSelf)
            {
                collisionObject = Instantiate(cookedMushroomPrefab, position, rotation);
                cookTimer.SetActive(false);
                overcookTimer.SetActive(true);  
                Begin(Duration, redFill, food);
            }
            else if(overcookTimer.activeSelf)
            {
                overcookTimer.SetActive(false);
                collisionObject = Instantiate(overCookedMushroomPrefab, position, rotation);
            }
        }
        else{
            Debug.Log(food);
        }
    }
}
