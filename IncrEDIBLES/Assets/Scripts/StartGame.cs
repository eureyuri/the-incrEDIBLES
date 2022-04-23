using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    // Initialize variable for kitchen scene.
    public string startGameScene;

    void Start()
    {

    }

    // Update is called once per frame.
    void Update()
    {

    }

    public void NewGame() {
        SceneManager.LoadScene(startGameScene);
    }
}
