using System;
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
        player = GameObject.Find("CapnGigi");
        buff = player.GetComponent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        activeList = GeneratePowerupList();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try 
        {
            if (activeList != null && activeList.Count > 0)
            {
                if (buff.TemporaryDash == true)
                {
                    try
                    {
                        // Check the Powerup List
                        for (int powerup = 0; powerup < activeList.Count; powerup++)
                        {
                            // If there is a powerup in the active list named DashPotion
                            if (activeList[powerup].name == "DashPotion")
                            {
                                if (activeList.Count <= 0) this.enabled = false;

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
                    catch (ArgumentOutOfRangeException aore)
                    {
                        Debug.Log(aore);
                    }
                }
                else if (buff.TemporaryAirDash == true)
                {
                    try
                    {
                        // Check the Powerup List
                        for (int powerup = 0; powerup < activeList.Count; powerup++)
                        {

                            // If there is a powerup in the active list named DashPotion
                            if (activeList[powerup].name == "AirDashPotion")
                            {
                                if (activeList.Count <= 0) this.enabled = false;

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
                    catch (ArgumentOutOfRangeException aore)
                    {
                        Debug.Log(aore);
                    }

                }
                else if (buff.TemporaryDoubleJump == true)
                {
                    try
                    {
                        // Check the Powerup List
                        for (int powerup = 0; powerup < activeList.Count; powerup++)
                        {
                            // If there is a powerup in the active list named DashPotion
                            if (activeList[powerup].name == "DoubleJumpPotion")
                            {
                                if (activeList.Count <= 0) this.enabled = false;

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
                    catch (ArgumentOutOfRangeException aore)
                    {
                        Debug.Log(aore);
                    }
                }
            } 
            else
            {
                if (powerups.Count <= 0)
                {
                    Debug.Log("Player has collected all available powerups, shutting down operations.");
                    PowerupWarehouse powerupWarehouse = new PowerupWarehouse();
                    powerupWarehouse.enabled = false;
                    this.enabled = false;
                }
            }
            
        }
        catch (NullReferenceException nre)
        {
            Debug.Log(nre);
        }
        finally
        {
            activeList.RemoveRange(0, powerups.Count);
            
            activeList = GeneratePowerupList();
        }
    }
       
}

