using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public void LoadLevel(string name) {
        Debug.Log("Loading level: "+name);
        SceneManager.LoadScene(name);
    }

    public void LoadNext() {
        Debug.Log("Loading Next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart() {
        Debug.Log("Restarting level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}