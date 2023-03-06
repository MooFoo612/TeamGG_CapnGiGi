using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timerScript : MonoBehaviour
{
    [SerializeField] private float myScore = 0f;
    public TextMeshProUGUI myGUIScore;
    private IEnumerator myCoroutine;


    void Start()
    {
        /* call the start coroutine method to run the my Countdown method
         * over a fixed time specified in the method, this allows the timer
         * to be counted up once every second*/
        //StartCoroutine(myCountdown());
        myCoroutine = myCountdown();

        StartCoroutine(myCoroutine);

        myGUIScore.text = myScore.ToString();
    }


    private IEnumerator myCountdown()
    {
        while (true)
        {


            yield return new WaitForSeconds(1); //wait 1 second

            myScore++;

            myGUIScore.text = "" + myScore;
            
        }
    }
}
