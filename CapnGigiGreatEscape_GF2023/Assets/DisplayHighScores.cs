using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour
{
    public Text[] highscoreText;
    leaderBoards highscoreManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". bleh...";
        }

        highscoreManager = GetComponent<leaderBoards>();

        StartCoroutine(RefreshHighscores());
    }

    public void OnHighscoresDownload(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if (highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
            
        }
    }

    public IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }


}
