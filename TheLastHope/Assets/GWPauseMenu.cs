using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GWPauseMenu : MonoBehaviour {
    public bool isPaused;
    public GameObject parent;
    public GameObject youDiedText;
    public Button resumeButton;

    public static GWPauseMenu instance;


    void Awake() {
        GWPauseMenu.instance = this;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyUp(KeyCode.P)) {
            this.ToggleGameState();
        }
    }

    public void ToggleGameState() {

        this.SetGamePaused(!this.isPaused);
    }

    public void SetGamePaused(bool state) {

        this.isPaused = state;
        this.parent.SetActive(state);

        if (state) {
            this.Pause();
        }
        else {
            this.Resume();
        }
    }

    public void SetGameDiedPaused() {
        this.isPaused = true;
        this.parent.SetActive(true);

        this.youDiedText.SetActive(true);
        this.resumeButton.gameObject.SetActive(false);
        this.Pause();
    }

    public void Restart() {
        this.Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Resume() {
        Time.timeScale = 1.0f;
    }

    public void Pause() {
        Time.timeScale = 0.0f;
    }
}
