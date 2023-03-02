using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    Debugging db;
    public GameObject blueDiamond;
    WorldGenerator worldGenerator;
    public bool reversed;

    #region List Declarations

    // Ground Chunk Prefabs
    public static List<GameObject> groundChunks; 

    // Platform Chunk Prefabs
    public static List<GameObject> platformChunks;

    // Enemy Prefabs
    public static List<GameObject> enemies;

    // Collectable Prefabs
    public static List<GameObject> collectables;

    // Coin Prefabs
    public static List<GameObject> coins;

    // Trap Prefabs
    public static List<GameObject> traps;

    // Background Images
    public static List<GameObject> backgrounds;
    
    // Treasure Prefabs
    public static List<GameObject> treasures;
    
    // Treasure Prefabs
    public static List<GameObject> powerups;

    #endregion

    #region Counters

    // Counts for Ground Sections Instantiated/Destroyed
    public static int platformChunkActivated = 0;
    public static int platformChunkDestroyed = 0;
    public static int groundChunkActivated = 0;
    public static int groundChunkDeactivated = 0;
    public static int chunksDeactivated = 0;

    public static int enemyKilled = 0;

    // Counts for Traps Instantiated/Destroyed
    public static int trapSpawned = 0;
    public static int trapDestroyed = 0;

    // Counts for Collectables Instantiated/Destroyed
    public static int collectableSpawned = 0;
    public static int collectableDestroyed = 0;
    public static int totalRunValue = 0;
    public static int actualRunValue = 0;

    #endregion

    // Most of the High-Priority Setup occurs in Awake()
    private void Awake()
    {
        // Ground Chunks
        groundChunks = GenerateGroundChunkList();

        // Platform Chunks
        platformChunks = GeneratePlatformList();

        // Enemies
        enemies = GenerateEnemyList();

        // Coins
        coins = GenerateCoinList();

        // Treasures
        treasures = GenerateTreasureList();

        // Collectables
        collectables = GenerateCollectableList();
        
        // Powerups
        powerups = GeneratePowerupList();

        // Traps
        traps = GenerateTrapList();

        // Backgrounds
        backgrounds = GenerateBackgroundList();
    }
    #region List & Array Generation


    // GroundChunks
    public List<GameObject> GenerateGroundChunkList()
    {
        groundChunks = new List<GameObject>(Resources.LoadAll<GameObject>("GroundChunks"));
        return new List<GameObject>(groundChunks);
    }

    // PlatformChunks
    public List<GameObject> GeneratePlatformList()
    {
        // Return new Platform List
        platformChunks = new List<GameObject>(Resources.LoadAll<GameObject>("PlatformChunks"));
        return new List<GameObject>(platformChunks);
    }

    // Enemies
    public List<GameObject> GenerateEnemyList()
    {
        // Return new Enemy List
        enemies = new List<GameObject>(Resources.LoadAll<GameObject>("Enemies"));
        return new List<GameObject>(enemies);
    }

    // Collectables
    public List<GameObject> GenerateCollectableList()
    {
        // Return new Collectable List
        collectables = new List<GameObject>(Resources.LoadAll<GameObject>("Collectables"));
        return new List<GameObject>(collectables);
    }

    public List<GameObject> GenerateCoinList()
    {
        // Return new Collectable List
        coins = new List<GameObject>(Resources.LoadAll<GameObject>("Coins"));
        return new List<GameObject>(coins);
    }

    // Traps
    public List<GameObject> GenerateTrapList()
    {
        // Return new Enemy List
        traps = new List<GameObject>(Resources.LoadAll<GameObject>("Traps"));
        return new List<GameObject>(traps);
    }

    public List<GameObject> GenerateBackgroundList()
    {
        // Return new Enemy List
        backgrounds = new List<GameObject>(Resources.LoadAll<GameObject>("Backgrounds"));
        return new List<GameObject>(backgrounds);
    }

    public List<GameObject> GenerateTreasureList()
    {
        // Return new Enemy List
        treasures = new List<GameObject>(Resources.LoadAll<GameObject>("Treasures"));
        return new List<GameObject>(treasures);
    }
    
    public List<GameObject> GeneratePowerupList()
    {
        // Return new Enemy List
        powerups = new List<GameObject>(Resources.LoadAll<GameObject>("Powerups"));
        return new List<GameObject>(powerups);
    }


    #endregion

    public void Update(){
        //reversed = worldGenerator.reversedWorld;
    }
} 
/*
public class CollectableWarehouse : Factory
{
    protected List<GameObject> _collectables;
    private int _listSize;
    
    protected List<GameObject> Collectables 
    {        
        get 
        {
            _collectables = collectables;
            return _collectables; 
        } 
        set 
        {
            _collectables = value;
        } 
    }
    
    protected int NumberOfCollectables 
    {        
        get 
        {
            _listSize = collectables.Count;
            return _listSize; 
        } 
    }
}

public class PowerupWarehouse : CollectableWarehouse
{
    protected List<GameObject> _powerups;
    private int _listSize;

    public List<GameObject> Powerups
    {
        get
        {
            _powerups = powerups;
            return _collectables;
        }
        set
        {
            _powerups = value;
        }
    }

    protected int NumberOfPowerups
    {
        get
        {
            _listSize = powerups.Count;
            return _listSize;
        }
    }
}

public class CoinWarehouse : CollectableWarehouse
{

    public Dictionary<string, int> new coins();
    public int _totalCoins;
    public int _coinsThisRun;

    protected int Coins
    {
        get
        {
            
            return _coins;
        }
        set
        {
            _coins = value;
        }
    }

    public int TotalCoins
    {
        get
        {
            _totalCoins = coins.Count;
            return _totalCoins;
        }
    }

    public int CoinsThisRun
    {
        get { return _coinsThisRun; }
    }
}*/
