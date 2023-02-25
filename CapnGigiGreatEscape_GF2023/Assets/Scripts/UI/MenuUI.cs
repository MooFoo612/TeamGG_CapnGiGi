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
        //PlayerPrefs.SetInt("swords", 10);
    }
    
    
}
