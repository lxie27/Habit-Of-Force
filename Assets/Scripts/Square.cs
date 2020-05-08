using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: potential combat-affecting property(s)


/*  This class defines an individual square and its properties.
 *  
 */

public class Square
{
    public Unit unit = null;           // Contained unit

    // Properties
    public bool isEmpty = true;

    public void addUnit(Unit unit)
    {
        this.unit = unit;
        isEmpty = false;
    }

    public void removeUnit()
    {
        this.unit = null;
        isEmpty = true;
    }
}
