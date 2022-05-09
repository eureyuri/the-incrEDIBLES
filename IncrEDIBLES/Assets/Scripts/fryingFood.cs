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
    public GameObject steakPrefab;
    public GameObject steakOvercookedPrefab;
    public GameObject cookedTomatoPrefab;
    public GameObject overCookedTomatoPrefab;
    public GameObject cookedFishPrefab;
    public GameObject overCookedFishPrefab;
    public GameObject cookedMushroomPrefab;
    public GameObject overCookedMushroomPrefab;
    public AudioSource audioSourceCooked;
    public AudioSource audioSourceOvercooked;

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
            collisionObject.GetComponent<OffsetGrabInteractable>().enabled = false;
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
        StartCoroutine(UpdateTimer(remainingDuration, uiFill, food));
    }

    private IEnumerator UpdateTimer(int remainingDuration, Image uiFill, string food)
    {
        while(remainingDuration>0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            Debug.Log("timer -1"+food);
            yield return new WaitForSeconds(0.2f);
            if(remainingDuration==0 && !overcookTimer.activeSelf && cookTimer.activeSelf)
            {
                Debug.Log("frying destroy");
                Destroy(collisionObject);
                yield return new WaitForSeconds(0.2f);
                OnCookedEnd(food);
                audioSourceCooked.Play();
            }
            else if(remainingDuration==0 && overcookTimer.activeSelf && !cookTimer.activeSelf)
            {
                Debug.Log("frying destroy2");
                Destroy(collisionObject);
                yield return new WaitForSeconds(0.2f);
                OnOvercookedEnd(food);
                audioSourceOvercooked.Play();
            }
        }

    }

    private void OnCookedEnd(string food)
    {
        if (food.Equals("steak"))
        {
            collisionObject = Instantiate(steakPrefab, position, rotation);
        }
        else if (food.Equals("tomato"))
        {
            collisionObject = Instantiate(cookedTomatoPrefab, position, rotation);
        }
        else if (food.Equals("fish"))
        {
            collisionObject = Instantiate(cookedFishPrefab, position, rotation);
        }
        else if (food.Equals("mushroom"))
        {
            collisionObject = Instantiate(cookedMushroomPrefab, position, rotation);
        }
        cookTimer.SetActive(false);
        overcookTimer.SetActive(true);
        Begin(Duration, redFill, food);
    }


    private void OnOvercookedEnd(string food)
    {
        overcookTimer.SetActive(false);
        if (food.Equals("steak"))
        {
            collisionObject = Instantiate(steakOvercookedPrefab, position, rotation);
        }
        else if (food.Equals("tomato"))
        {
            collisionObject = Instantiate(overCookedTomatoPrefab, position, rotation);
        }
        else if (food.Equals("fish"))
        {
            collisionObject = Instantiate(overCookedFishPrefab, position, rotation);
        }
        else if (food.Equals("mushroom"))
        {
            collisionObject = Instantiate(overCookedMushroomPrefab, position, rotation);
        }
    }
}
