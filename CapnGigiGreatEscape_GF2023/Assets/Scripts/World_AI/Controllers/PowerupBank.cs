using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBank : Factory
{
    protected List<GameObject> activeList;
    protected PlayerInventory buff;
    GameObject player;

    private void Awake()
    {
        activeList = GeneratePowerupList();
        player = GameObject.Find("CapnGigi");
        buff = player.GetComponent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activeList != null)
        {
            if (buff.TemporaryDash == true)
            {
                if (activeList != null)
                {
                    // Check the Powerup List
                    for (int powerup = 0; powerup < activeList.Count; powerup++)
                    {
                        // If there is a powerup in the active list named DashPotion
                        if (activeList[powerup].name == "DashPotion")
                        {
                            // Remove the item from the global list in ListFactory
                            powerups.RemoveAt(powerup);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else if (buff.TemporaryAirDash == true)
            {
                if (activeList != null)
                {
                    // Check the Powerup List
                    for (int powerup = 0; powerup < activeList.Count; powerup++)
                    {
                        // If there is a powerup in the active list named DashPotion
                        if (activeList[powerup].name == "AirDashPotion")
                        {
                            // Remove the item from the global list in ListFactory
                            powerups.RemoveAt(powerup);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    return;
                }

            }
            else if (buff.TemporaryAirDash == true)
            {
                if (activeList != null)
                {
                    // Check the Powerup List
                    for (int powerup = 0; powerup < activeList.Count; powerup++)
                    {
                        // If there is a powerup in the active list named DashPotion
                        if (activeList[powerup].name == "DoubleJumpPotion")
                        {
                            // Remove the item from the global list in ListFactory
                            powerups.RemoveAt(powerup);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }                    
    }
       
}

