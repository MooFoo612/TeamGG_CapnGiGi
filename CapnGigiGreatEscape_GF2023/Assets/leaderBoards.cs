using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class leaderBoards : MonoBehaviour
{
    const string privateCode = "fYR0qF1TlkyxeVN8jk2LhgmxvzTGipjEmhbi_bVLI0cg";
    const string publicCode = "6407066b778d3c8a30d47ae1";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoreList;
    DisplayHighScores highscoresDisplay;

    void OnEnable()
    {
        highscoresDisplay = GetComponent<DisplayHighScores>();
        DownloadHighScores();
    }

    public void DownloadHighScores()
    {
        StartCoroutine(DownloadHighScoreFromDatabase());
    }

    private IEnumerator DownloadHighScoreFromDatabase()
    {
        Debug.Log("Downloading the name and score");

        string loaded = (webURL + publicCode + "/pipe");
        
        UnityWebRequest uwr = UnityWebRequest.Get(loaded);
        yield return uwr.SendWebRequest();
        
        Debug.Log("just downloaded: " + uwr);

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error Downloading" + uwr.error);
        }
        else if (uwr.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.Log("data process error" + uwr.error);
        }
        else
        {

            FormatHighScores(uwr.downloadHandler.text);
            Debug.Log("formatting");
            highscoresDisplay.OnHighscoresDownload(highscoreList);

        }
    }

    public void FormatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoreList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highscoreList[i] = new Highscore(username, score);

            print(highscoreList[i].username + ", " + highscoreList[i].score);
        }
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}
