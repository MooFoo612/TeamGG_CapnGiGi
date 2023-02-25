using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject menuUI;
    public void ShopButton(){
        menuUI.SetActive(false);
        shopUI.SetActive(true);
    }
    public void PlayButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    void Start(){
        // Reset Playerprefs (decomment all run and recomment)
        //PlayerPrefs.SetInt("swords", 10);
        //PlayerPrefs.SetInt("purchasedDoubleJump", 0);
        //PlayerPrefs.SetInt("purchasedDash", 0);
        //PlayerPrefs.SetInt("purchasedAirDash", 0);
        
    }
    
    
}
