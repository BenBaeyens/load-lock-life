using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour {

    public AudioClip hoversound;
    public AudioClip clicksound;

    public bool inGame = false;
    private bool paused = false;
    public GameObject pauseMenu;

    AudioSource audioSource;

    public TextMeshProUGUI highscore;

    private void Start() {
        highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore").ToString();
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && inGame)
        {
            Debug.Log("test");
            if (paused)
            {
                UnPauseGame();
            }
            if (!paused)
            {
                Debug.Log("paused");
                PauseGame();
            }
        }
    }

    public void SelectDifficulty(string d) {
        if(d == "hitman")
            PlayerPrefs.SetInt("difficulty", 3);
        if (d == "beginner")
            PlayerPrefs.SetInt("difficulty", 1);
        if (d == "hobbyist")
            PlayerPrefs.SetInt("difficulty", 2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit() {
        Application.Quit();
    }

    public void hoverSound() {
        audioSource.PlayOneShot(hoversound);
    }
    public void clickSound() {
        audioSource.PlayOneShot(clicksound);
    }

    public void mainMenu() {
        SceneManager.LoadScene(0);
    }
    public void PlayAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void PauseGame() {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void UnPauseGame() {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    
}
