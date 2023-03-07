using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] Button closeControlsButton;
    [SerializeField] GameObject controlsUI;
    [SerializeField] GameObject menuUI;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    public void CloseControls()
    {
        PlayerPrefs.SetInt("fromShop", 1);
        // Reload the scene
        //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        controlsUI.SetActive(false);
        menuUI.SetActive(true);

    }
}
