using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    // Initialize variable for kitchen scene.
    public string startGameScene;

    public void NewGame() {
        Score.reset();
        SceneManager.LoadScene(startGameScene);
    }
}
