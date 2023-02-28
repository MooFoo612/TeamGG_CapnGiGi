using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOutOfCamera : MonoBehaviour
{
    public GameObject theGameObject;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        theGameObject = this.gameObject;
        //player = GameObject.Find("CapnGigi");
        StartCoroutine(myCheck());
        anim = GetComponent<Animator>();
    }

    private IEnumerator myCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (theGameObject.transform.position.y <= -7)
            {
                anim.SetBool(AnimationStrings.isAlive, false);
                //FindObjectOfType<Damageable>().changeIsAlive();
            }
        }
    }
}
