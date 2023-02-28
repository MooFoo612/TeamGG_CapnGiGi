using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : ListFactory
{
    private Vector3 spawnLocation;
    private Transform coinParent;

    private void Awake()
    {
        coinParent = GameObject.Find("Coins_Active").transform;
        spawnLocation = gameObject.transform.position;
    }
    private void Start()
    {
        GenerateRandomCoin(coins, spawnLocation, coinParent);
    }
    private void GenerateRandomCoin(List<GameObject> coinList, Vector3 spawnLocation, Transform coinParent)
    {
        int randomCoin = UnityEngine.Random.Range(0, coinList.Count - 1);
        Instantiate(coinList[randomCoin], spawnLocation, Quaternion.identity, coinParent);
    }
}
