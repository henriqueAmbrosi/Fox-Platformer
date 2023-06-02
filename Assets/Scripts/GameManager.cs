using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int gems = 0;
    public Text gemCounterText;
    public int totalGems;
    Scene scn;
    bool isPaused = false;
    public static GameManager gm;

    public GameObject pausePanel, endLevelPanel;

    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        gemCounterText.text = gems.ToString("00");
        scn = SceneManager.GetActiveScene();
        pausePanel.SetActive(false);
        endLevelPanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            pauseGame();
        }
    }

    public void AddGem()
    {
        gems++;
        gemCounterText.text = gems.ToString("00");
        if(gems >= totalGems)
        {
            PlayerPrefs.SetInt("Level" + (scn.buildIndex + 1).ToString() + "Unlocked", 1);
            endLevelPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene((scn.buildIndex + 1) % 5);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(scn.buildIndex);
    }

    public void pauseGame()
    {
        if (!isPaused)
        {
            // Pausa
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
        else
        {
            // Despausa
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
