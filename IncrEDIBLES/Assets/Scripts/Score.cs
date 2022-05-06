using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static readonly int decrementVal = -10;

    public static void adjust(int delta) {
        score += delta;
    }

    public static void reset() {
        score = 0;
     }
}
