using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void Restart() {
        SceneManager.LoadScene(1);
    }

    public static void SceneById(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }
}
