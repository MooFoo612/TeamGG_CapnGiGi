using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GUIPanel;
    public GameObject GameOverPanel;
    public TMP_Text coinsText;
    public TMP_Text diamondsText;
    PlayerInventory playerInv;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject menuUI;





    
    Animator anim ;


    public bool isAlive{
        get{
            return anim.GetBool(AnimationStrings.isAlive);
        }
    }
    void Awake(){
        anim = GetComponent<Animator>();
        playerInv = GetComponent<PlayerInventory>();
    }
    
    void PauseGame()
    {
        Time.timeScale = 0;
    }
    
    void ResumeGame()
    {
        Time.timeScale = 1;
    }
    
    public void Update(){
        // When player dies pause the game and open the Game Over panel
        if(!isAlive){
            PauseGame();
            GUIPanel.SetActive(false);
            GameOverPanel.SetActive(true);
            // Update the coins and the diamonds text
            coinsText.text = "X " + playerInv.Coins;
            diamondsText.text = "X " + playerInv.Diamonds;
        }
    }

    public void ExitButton(){
        // Update the values for the shop
        PlayerPrefs.SetInt("coins", (PlayerPrefs.GetInt("coins") + playerInv.Coins));
        PlayerPrefs.SetInt("diamonds", (PlayerPrefs.GetInt("diamonds") + playerInv.Diamonds));
        // Go to menu
        menuUI.SetActive(true);
        //GameOverPanel.SetActive(false);
        //SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void ShopButton(){
        // Update the values for the shop
        PlayerPrefs.SetInt("coins", (PlayerPrefs.GetInt("coins") + playerInv.Coins));
        PlayerPrefs.SetInt("diamonds", (PlayerPrefs.GetInt("diamonds") + playerInv.Diamonds));
        // Open the shop
        shopUI.SetActive(true);
    }

    public void PlayAgainButton(){
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        menuUI.SetActive(false);
        ResumeGame();
    }
}
