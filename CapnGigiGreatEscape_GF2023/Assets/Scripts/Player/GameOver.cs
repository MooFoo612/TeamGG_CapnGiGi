using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject GUIPanel;
    public GameObject GameOverPanel;
    Animator anim ;


    public bool isAlive{
        get{
            return anim.GetBool(AnimationStrings.isAlive);
        }
    }
    void Awake(){
        anim = GetComponent<Animator>();
    }

    
    void PauseGame ()
    {
        Time.timeScale = 0;
    }
    void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    public void Update(){
        if(!isAlive){
            PauseGame();
            GUIPanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }
    }
}
