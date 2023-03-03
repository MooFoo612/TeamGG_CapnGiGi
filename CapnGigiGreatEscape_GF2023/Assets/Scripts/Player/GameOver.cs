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
    public TMP_Text coinsText;
    public TMP_Text diamondsText;
    PlayerInventory playerInv;
    [SerializeField] GameObject shopUI;
    public GameObject gameOverUI;
    [SerializeField] GameObject menuUI;
    //public GameObject miniMap;
    
    Animator anim ;

    private void Start()
    {
        //miniMap = GameObject.FindWithTag("miniMap");
        //miniMap.SetActive(false);
    }

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
        //miniMap.SetActive(false);
    }
    
    void ResumeGame()
    {
        Time.timeScale = 1;
        //miniMap.SetActive(true);
    }
    
    public void Update(){
        // When player dies pause the game and open the Game Over panel
        if(!isAlive){
            PauseGame();
            GUIPanel.SetActive(false);
            gameOverUI.SetActive(true);
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
        //menuUI.SetActive(true);
        // Reload the scene
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        //GameOverPanel.SetActive(false);
        //SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        //miniMap.SetActive(false);
    }

    public void ShopButton(){
        // Update the values for the shop
        PlayerPrefs.SetInt("coins", (PlayerPrefs.GetInt("coins") + playerInv.Coins));
        PlayerPrefs.SetInt("diamonds", (PlayerPrefs.GetInt("diamonds") + playerInv.Diamonds));
        // Open the shop
        shopUI.SetActive(true);
        //miniMap.SetActive(false);
    }

    public void PlayAgainButton(){
        // Set this to not opoen the menu screen and rerun the game directly
        PlayerPrefs.SetInt("alreadyRunned", 1);
        // Close the game over screen 
        gameOverUI.SetActive(false);
        // Reload the scene
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        //menuUI.SetActive(false);
        //ResumeGame();
        //miniMap.SetActive(true);
    }
}
