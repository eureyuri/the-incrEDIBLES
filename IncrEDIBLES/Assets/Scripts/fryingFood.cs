using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log("Steak put in frying pan");
            cookTimer.SetActive(true);
            newPrefab = steakPrefab;
            Begin(Duration, greenFill);
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
        OnEnd();
        if(cookTimer.activeSelf)
        {
            cookTimer.SetActive(false);
            overcookTimer.SetActive(true);
            newPrefab = steakOvercookedPrefab;
            Begin(Duration, redFill);
        }
        else 
        {
            overcookTimer.SetActive(false);
        }
        
    }

    private void OnEnd()
    {
        Destroy(collisionObject);
        collisionObject = Instantiate(newPrefab, position, rotation);
    }
}
