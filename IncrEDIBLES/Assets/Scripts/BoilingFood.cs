using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoilingFood : MonoBehaviour
{
    [SerializeField] public Image redFill;
    [SerializeField] public Image greenFill;
    public GameObject overcookTimer;
    public GameObject cookTimer;
    private int Duration = 90;
    private int remainingDuration;
    private Vector3 position;
    private Quaternion rotation;
    private GameObject collisionObject;
    private GameObject newPrefab;
    public GameObject pastaCookedPrefab;
    public GameObject pastaOvercookedPrefab;

    void Start()
    {
        cookTimer.SetActive(false);
        overcookTimer.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Pasta put to boil");
        collisionObject = collision.gameObject;
        position = collisionObject.transform.position;
        rotation = collisionObject.transform.rotation;

        if (collision.gameObject.CompareTag("Pasta"))
        {
            cookTimer.SetActive(true);
            newPrefab = pastaCookedPrefab;
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
            newPrefab = pastaOvercookedPrefab;
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
