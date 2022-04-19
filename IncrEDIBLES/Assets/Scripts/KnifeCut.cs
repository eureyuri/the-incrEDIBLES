using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class KnifeCut : MonoBehaviour
{
    public Text cut_num;
    public Text cut_num1;
    public Text cut_num2;

    public GameObject cutBoard;
    public GameObject cutBoard1;
    public GameObject cutBoard2;

    // Update is called once per frame
    public void decrement()
    {
        float posX = transform.localPosition.x;
        float posY = transform.localPosition.y;
        float posZ = transform.localPosition.z;
        // if(posX > -0.5f && posX < 0.5f && posY > 0 && posY < 0.3f && posZ > -0.4f && posZ < 0.3f)
        // {
        if(gameObject.CompareTag("knife") && cut_num.enabled == true)
        {
            int current = int.Parse(cut_num.text);
            if(current == 1)
            {
                cut_num.text = "3";
                cut_num.enabled = false;
                GameObject child = cutBoard.transform.GetChild(0).gameObject;
                child.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                cut_num.text = (current - 1).ToString();
            }
        }
        else if(gameObject.CompareTag("knife1") && cut_num1.enabled == true)
        {
            int current = int.Parse(cut_num.text);
            if(current == 1)
            {
                cut_num1.text = "3";
                cut_num1.enabled = false;
                GameObject child = cutBoard1.transform.GetChild(0).gameObject;
                child.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                cut_num1.text = (current - 1).ToString();
            }
        }
        else if(gameObject.CompareTag("knife2") && cut_num2.enabled == true)
        {
            int current = int.Parse(cut_num.text);
            if(current == 1)
            {
                cut_num2.text = "3";
                cut_num2.enabled = false;
                GameObject child = cutBoard1.transform.GetChild(0).gameObject;
                child.GetComponent<XRGrabInteractable>().enabled = true;
            }
            else
            {
                cut_num2.text = (current - 1).ToString();
            }
        }
        // }
    }
}
