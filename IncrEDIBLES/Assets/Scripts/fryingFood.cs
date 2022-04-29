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
        if (collision.gameObject.CompareTag("Steak"))
        {
            Debug.Log("Steak in frying pan");
            cookTimer.SetActive(true);
            collisionObject.GetComponent<XRGrabInteractable>().enabled = false;
            newPrefab = steakPrefab;
            Begin(Duration, greenFill);
        }
        else if (collision.gameObject.CompareTag("CutTomato"))
        {
            Debug.Log("Tomato in frying pan");
            cookTimer.SetActive(true);
            collisionObject.GetComponent<XRGrabInteractable>().enabled = false;
            newPrefab = cookedTomatoPrefab;
            Begin(Duration, greenFill);
        }
        /*
        if (collision.gameObject.CompareTag("Steak") || collision.gameObject.CompareTag("CutTomato"))
        {
            Debug.Log("Food in frying pan");
            cookTimer.SetActive(true);
            collisionObject.GetComponent<XRGrabInteractable>().enabled = false;
            if (collision.gameObject.CompareTag("Steak"))
            {
                newPrefab = steakPrefab;
            }
            else if (collision.gameObject.CompareTag("CutTomato"))
            {
                newPrefab = cookedTomatoPrefab;
            }
            Begin(Duration, greenFill);
        }
        */

    }

     void OnCollisionExit(Collision collision)
    {
        if((collision.gameObject.CompareTag("ReadyMeat") || collision.gameObject.CompareTag("ReadyTomato")) && overcookTimer.activeSelf)
        {
            overcookTimer.SetActive(false);
        }
    }

    private void Begin(int Second, Image uiFill)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer(uiFill));
    }

    private IEnumerator UpdateTimer(Image uiFill)
    {
        while(remainingDuration>0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            Debug.Log("timer -1");
            yield return new WaitForSeconds(0.2f);
        }
        if(overcookTimer.activeSelf)
        {
            overcookTimer.SetActive(false);
            OnEnd();
        }
        else if(newPrefab.CompareTag("ReadyTomato"))
        {
            OnEnd();
            newPrefab = overCookedTomatoPrefab;
            cookTimer.SetActive(false);
            overcookTimer.SetActive(true);  
            Begin(Duration, redFill);
        }
        else if(newPrefab.CompareTag("ReadyMeat"))
        {
            OnEnd();
            newPrefab = steakOvercookedPrefab;
            cookTimer.SetActive(false);
            overcookTimer.SetActive(true);  
            Begin(Duration, redFill);
        }
        
    }

    private void OnEnd()
    {
        Destroy(collisionObject);
        collisionObject = Instantiate(newPrefab, position, rotation);
    }
}
