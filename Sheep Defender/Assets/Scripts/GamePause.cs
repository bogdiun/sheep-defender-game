using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//remember that changing timeScale is Global (not reset on new scene)
public class GamePause : MonoBehaviour {
    public static bool Enabled = false;
    public GameObject pauseMenu;
    
    private void Start() {
        Time.timeScale = 1f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Enabled) Resume();
            else Pause();
        }
    }

    public void Pause() {
        Enabled = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        Debug.Log("Pause is " + Enabled);
    }

    public void Resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Enabled = false;

        Debug.Log("Pause is " + Enabled);
    }
}
