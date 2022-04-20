using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class foodTimer : MonoBehaviour
{
    private Image uiFill;
    public int Duration = 10;
    private int remainingDuration;
    // Start is called before the first frame update
    void onEnable()
    {
        Begin(Duration);
    }

    private void Begin(int Second)
    {
        remainingDuration = Second;
        StartCoroutine(UpdateTimer());
    }
    // Update is called once per frame
    private IEnumerator UpdateTimer()
    {
        while(remainingDuration>=0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }
        OnEnd();
    }

    private void OnEnd()
    {
        //this.SetActive(false);
    }
}
