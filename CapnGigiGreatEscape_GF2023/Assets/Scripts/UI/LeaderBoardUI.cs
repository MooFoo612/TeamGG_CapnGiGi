using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{
    
    [SerializeField] GameObject leaderBoardUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] Button openLeaderBoardButton;
    [SerializeField] Button closeLeaderBoardButton;

    

    private void Start()
    {
        Time.timeScale = 0;
    }

   

    public void Update()
    {
        
    }
    
    public void CloseLeaderBoard()
    {
        leaderBoardUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
