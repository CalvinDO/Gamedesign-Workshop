using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWPauseMenu : MonoBehaviour {
    public bool isPaused;
    public GameObject parent;


    // Update is called once per frame
    void Update() {

        if (Input.GetKeyUp(KeyCode.Escape)) {
            this.ToggleGameState();
        }
    }

    public void ToggleGameState() {

        this.isPaused = !this.isPaused;

        this.parent.SetActive(this.isPaused);

        this.SetGamePaused(this.isPaused);
    }

    public void SetGamePaused(bool state) {

        if (state) {
            this.Pause();
        }
        else {
            this.Resume();
        }
    }

    public void Resume() {
        Time.timeScale = 1.0f;
    }

    public void Pause() {
        Time.timeScale = 0.0f;
    }
}
