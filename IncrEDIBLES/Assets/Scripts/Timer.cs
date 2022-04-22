using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float initialTime;
    private static float timeRemaining;
    private static TimerState timerState;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = initialTime;
        timerState = TimerState.GOOD;
    }

    void Update()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= initialTime * 1 / 3) {
                timerState = TimerState.BAD;
            } else if (timeRemaining <= initialTime * 2 / 3) {
                timerState = TimerState.NORMAL;
            }
        }
    }

    public static float getRemainingTime() {
        return timeRemaining;
    }

    public static TimerState getTimerState() {
        return timerState;
    }
}
