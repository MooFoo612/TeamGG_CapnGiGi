using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject miniMap;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] GameObject leaderBoard;
    [SerializeField] GameObject controlsCanvas;
    public void ShopButton(){
        menuUI.SetActive(false);
        shopUI.SetActive(true);
        GUI.SetActive(false);
        miniMap.SetActive(false);
        scoreCanvas.SetActive(false);
        controlsCanvas.SetActive(false);
        leaderBoard.SetActive(false);
    }

    public void LeaderBoardButton()
    {
        menuUI.SetActive(false);
        shopUI.SetActive(false);
        GUI.SetActive(false);
        miniMap.SetActive(false);
        scoreCanvas.SetActive(false);
        leaderBoard.SetActive(true);
    }

    public void ControlsButton(){
        menuUI.SetActive(false);
        shopUI.SetActive(false);
        GUI.SetActive(false);
        miniMap.SetActive(false);
        scoreCanvas.SetActive(false);
        controlsCanvas.SetActive(true);
    }

    public void PlayButton(){
        
        menuUI.SetActive(false);
        shopUI.SetActive(false);
        GUI.SetActive(true);
        miniMap.SetActive(true);
        scoreCanvas.SetActive(true);
        leaderBoard.SetActive(false);
        leaderBoard.SetActive(false);
        controlsCanvas.SetActive(false);
        //ResumeGame();

        if (PlayerPrefs.GetInt("fromShop") == 1){
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            PlayerPrefs.SetInt("fromShop", 0);
        } else {
            ResumeGame();
        }
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

        // Reset Playerprefs (decomment all run and recomment)
        
        if(PlayerPrefs.GetInt("alreadyRunned") == 0){   
            // Pause game and interact with the menu 
            PauseGame();
            menuUI.SetActive(true);
            GUI.SetActive(false);
            shopUI.SetActive(false);
            miniMap.SetActive(false);
            scoreCanvas.SetActive(false);
            leaderBoard.SetActive(false);
            controlsCanvas.SetActive(false);
        }
        
        if(PlayerPrefs.GetInt("alreadyRunned") == 1 ){
            // Run the game directly from the gameover screen 
            menuUI.SetActive(false);
            miniMap.SetActive(true);
            scoreCanvas.SetActive(true);
            ResumeGame();
            PlayerPrefs.SetInt("alreadyRunned", 0);
            PlayerPrefs.SetInt("fromShop", 0);
        }

        PlayerPrefs.SetInt("swords", 10);
        PlayerPrefs.SetInt("coins", 5000);
        PlayerPrefs.SetInt("diamonds", 50);
        PlayerPrefs.SetInt("purchasedDoubleJump", 0);
        PlayerPrefs.SetInt("purchasedDash", 0);
        PlayerPrefs.SetInt("purchasedAirDash", 0);
        PlayerPrefs.SetInt("swordAttackPowerUp", 0);
        PlayerPrefs.SetInt("throwSwordAttackPowerUp", 0);
        
        
        //PlayerPrefs.SetInt("alreadyRunned", 0);


        PlayerPrefs.SetInt("coinsCountdown", 10);


        

    }
    
    
    
}
