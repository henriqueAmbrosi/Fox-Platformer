using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManager : MonoBehaviour
{

    public GameObject mainMenu, levelSelect;
    public Button[] lvlBtns;

    // Start is called before the first frame update
    void Start()
    {
        MainMenu();
        
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        checkLevels();
    }

    public void LoadLevel(int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
    }

    void checkLevels()
    {
        for(int i = 1; i < lvlBtns.Length; i++)
        {
            if (PlayerPrefs.HasKey("Level" + (i + 1).ToString() + "Unlocked"))
            {
                lvlBtns[i].interactable = true;
            }
            else
            {
                lvlBtns[i].interactable = false;
            }
        }
    }

    public void ResetLvlsUnlocked()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
