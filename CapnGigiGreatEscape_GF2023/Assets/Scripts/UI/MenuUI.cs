using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject GUI;
    public void ShopButton(){
        menuUI.SetActive(false);
        shopUI.SetActive(true);
        GUI.SetActive(false);
    }
    public void PlayButton(){
        menuUI.SetActive(false);
        shopUI.SetActive(false);
        ResumeGame();
        GUI.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton(){
        Application.Quit();
    }

    void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
    
    void Start(){
        // Pause game and interact with the menu 
        PauseGame();
        menuUI.SetActive(true);
        GUI.SetActive(false);
        shopUI.SetActive(false);

        // Reset Playerprefs (decomment all run and recomment)
        //PlayerPrefs.SetInt("swords", 10);
        //PlayerPrefs.SetInt("coins", 500);
        //PlayerPrefs.SetInt("diamonds", 250);
        //PlayerPrefs.SetInt("purchasedDoubleJump", 0);
        //PlayerPrefs.SetInt("purchasedDash", 0);
        //PlayerPrefs.SetInt("purchasedAirDash", 0);

    }
    
    
    
}
