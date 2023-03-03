using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{ 
    private GameObject player;
    private float lastPlayerX;
    private float currentPlayerX;
    private float checkPlayerDirection;

    public float speedX;
    public float speedY;

    [SerializeField] private Renderer bgRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CapnGigi");
    }

    
    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(speedX*Time.deltaTime,0);     
    }

    private void LateUpdate()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(-speedX * Time.deltaTime, 0);
        //lastPlayerX = currentPlayerX;
    }
}
