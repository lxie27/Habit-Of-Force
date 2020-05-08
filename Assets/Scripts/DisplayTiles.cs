/* 
 * Author: Leslie Xie
 * Algorithm for creating a tactics game style movement and attack range display
 * 
 * TODOS:
 *  Do the arrow thing for least expensive path
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTiles : MonoBehaviour
{
    public GameObject moveTile;         //The blue tiles showing range of movement
    public GameObject attackTile;       //The red tiles showing the limits of attack
    public int moveRange;               //The movement range of the source unit
    public int attackRange;             //The attack range of the source unit

    public GameObject source;           //The source of the tiles; usually a moveable unit
    public GameObject totalTiles;       //Temporary game object parented under source, can be toggled on/off
    
    /*
     * This function creates a filled in square of tiles around the totalTiles gameObject.
     * TODO: make it not so filled in if there's blockages/enemies
     */
    public void generate(GameObject sourceUnit)
    {
        source = sourceUnit;
        totalTiles = new GameObject("Tile Display");
        Debug.Log("parenting totaltiles to " + source.gameObject.name);
        totalTiles.transform.SetParent(source.transform);
        totalTiles.transform.localPosition = new Vector3(0, 0, 0);
        int radius = 3;
        while (radius >= 0)
        {
            genRing(radius);
            radius--;
        }
        genMoveTile(0, 0);              //last tile underneath the source

    }


    //TODO: instead of regenerating and destroying everytime, save the totaltiles unless the position/ranges of the object has been changed,
    // more like a "hide" method
    public void destroy()
    {
        Debug.Log("Destroying tiles");
        Destroy(totalTiles);
    }

    /*
     * This function creates the ring of movement tiles at the given radius
     * radius = the distance from (0,0)
     */
    void genRing(int radius)
    {
        int vertical = 0;
        while (radius > 0)
        {
            genMoveTile(radius, vertical);
            genMoveTile(-radius, -vertical);
            genMoveTile(vertical, -radius);
            genMoveTile(-vertical, radius);

            radius--;
            vertical++;
        }
    }
    
    // This function creates an instance of the movement tile with an offset by x, z.
    void genMoveTile(int x, int z)
    {
        Instantiate(moveTile, new Vector3(totalTiles.transform.position.x + x, 0.004f, totalTiles.transform.position.z + z), Quaternion.identity, totalTiles.transform);
    }
}
