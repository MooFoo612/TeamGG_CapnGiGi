using System.Collections.Generic;
using UnityEngine;

public class CoinFactory : ListFactory
{
    //private ListFactory lf;
    //private List<GameObject> coinList;
    private Vector3 spawnLocation;

    private void Awake()
    {
        spawnLocation = gameObject.transform.position;
        //lf = gameObject.AddComponent<ListFactory>();
        //coinList = lf.GenerateEnemyList();
    }
    private void Start()
    {
        GenerateRandomCoin(coinPrefabs, spawnLocation);
    }
    private void GenerateRandomCoin(List<GameObject> coinList, Vector3 spawnLocation)
    {
        int randomCoin = UnityEngine.Random.Range(0, coinList.Count - 1);
        Instantiate(coinList[randomCoin], spawnLocation, Quaternion.identity);
    }
}
