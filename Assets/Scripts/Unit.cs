using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: probably some refactoring to split mecha and human into separate class and combine into this one

public enum Affiliation             //Factions for Units
{
    ally, enemy, unaffiliated
}

public class Unit
{
    public int healthpoints;        //HP: 10 is default
    public string name;             //Name
    public bool status;             //Status: dead is false, alive is true
    public int moveRange;           //Distance unit can move
    public int attackRange;         //Distance unit can attack

    public GameObject model;        //Unit's model/skin
    public Affiliation side;        //Faction
    public Vector2 position;        //Position (x,y) of this unit

    //Mecha stats
    public int speed;               //Initiative and other misc checks
    public int attackDamage;        //Current attack damage
    public int armor;               //Current armor value

    //Human stats
    public int accuracy;            //Likelihood of hitting an attack
    public int critRate;            //Likelihood of landing a critical hit
    public int dodge;               //Likelihood of nullifying an incoming attack/skill
    
    //Constructor given an Affiliation
    public Unit(Affiliation side)
    {
        if (side == Affiliation.ally)
        {
            model = (GameObject)Resources.Load("Prefabs/CustomUnits/Mech_White");
        }
        else if (side == Affiliation.enemy)
        {
            model = (GameObject)Resources.Load("Prefabs/CustomUnits/Mech_Red");
        }
        else if (side == Affiliation.unaffiliated)
        {
            //placeholder for "Other" faction
        }

        this.side = side;

        if (healthpoints == 0)
        {
            healthpoints = 10;
        }

        status = true;
    }

    //Adds delta to health (use negative values to subtract)
    //Returns current health
    int changeHealth(int delta)
    {
        healthpoints += delta;
        return healthpoints;
    }
}
