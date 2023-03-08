using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{

    [SerializeField] GameObject infoUI;
    [SerializeField] GameObject menuUI;
    [SerializeField] Button openInfoButton;
    [SerializeField] Button closeInfoButton;


    private void Start()
    {
        Time.timeScale = 0;
    }


    public void Update()
    {

    }

    public void CloseInfo()
    {
        infoUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
