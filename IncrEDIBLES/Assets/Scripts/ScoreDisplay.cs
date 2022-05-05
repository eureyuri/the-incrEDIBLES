using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreField;
    // Start is called before the first frame update
    void Start()
    {
        scoreField.text = Score.score.ToString(); 
    }
}
