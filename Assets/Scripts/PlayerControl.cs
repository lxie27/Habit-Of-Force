using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* This class defines all of the player's controls and what player can do during combat.
 * 
 * Inputs:
 *      Left Arrow - moves cursor to world left (x-=1)
 *      Right Arrow - moves cursor to world right (x+=1)
 *      Up Arrow - moves cursor to world forward (z+=1)
 *      Down Arrow - moves cursor to world backward (z-=1)
 *      Z - acts on objects in the bounds of the Cursor game object
 */

public class PlayerControl : MonoBehaviour
{
    public GameObject cursor;       //The little square the player moves to select things with
    
    Unit hoveredUnit;               //Unit the cursor is *hovering* over (just looking at it)
    Unit selectedUnit;              //Unit the player has selected (pressed Z on)

    bool unitSelected = false;      //Flag for when the player is selecting a unit
    public bool playerTurn = true;  //True for player's turn; false for enemy turn
    
    DisplayTiles display;           //Class for showing movement/attack tiles after unit selection
    
    Board gameBoard;                //The board containing the squares and methods

    // Start is called before the first frame update
    void Start()
    {
        //Test board
        gameBoard = new Board();
        gameBoard = gameBoard.generateTestBoard(8);
        //End of testing

        display = this.gameObject.transform.parent.GetComponent<DisplayTiles>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if cursor hovers over a unit
        if (gameBoard.squareHasItem(cursor.transform.position))
        {
            hoveredUnit = gameBoard.squares[(int)cursor.transform.position.x, (int)cursor.transform.position.z].unit;
        }
        else
        {
            hoveredUnit = null;
        }

        //moving around the cursor
        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow))
            {
                //if player is selecting a unit
                if (unitSelected)
                {
                    //TODO: movement after the whole arrow with shortest path thing (fire emblem-esque)
                    Movement(null);
                }
                //otherwise just moving the cursor around
                else
                {
                    //bounding cursor to board boundaries
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) && cursor.transform.position.x != 0)
                        || (Input.GetKeyDown(KeyCode.RightArrow) && cursor.transform.position.x != gameBoard.boardSize - 1)
                        || (Input.GetKeyDown(KeyCode.UpArrow) && cursor.transform.position.z != gameBoard.boardSize - 1)
                        || (Input.GetKeyDown(KeyCode.DownArrow) && cursor.transform.position.z != 0))
                    {
                        Movement(null);
                    }
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (unitSelected)
                {
                    //move held unit to new square

                    selectedUnit = null;
                    display.destroy();
                    unitSelected = !unitSelected;

                }
                else if (!unitSelected)
                {
                    if (hoveredUnit.side == Affiliation.ally)
                    {
                        selectedUnit = hoveredUnit;
                        unitSelected = !unitSelected;
                        display.generate(selectedUnit.model);

                        //TODO: grab move and attack range from unit

                    }
                    else if (hoveredUnit.side == Affiliation.enemy)
                    {
                        //something with enemy info?
                    }
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerTurn = false;
        }
    }

    bool DropUnitOff(Vector3 pos, GameObject item)
    {
        if (gameBoard.squares[(int)pos.x, (int)pos.y].isEmpty)
        {
            return true;
        }
        return false;
    }

    void DisplayAttackMenu(GameObject unit)
    {

    }

    void DisplayMenu(Transform menu)
    {
        menu.gameObject.SetActive(true);
    }

    void UndisplayMenu(Transform menu)
    {
        menu.gameObject.SetActive(false);
    }

    //Wrapper for Units to be moved
    void Movement(GameObject unit)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft(cursor.transform);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight(cursor.transform);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp(cursor.transform);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown(cursor.transform);
        }
    }

    // Methods for moving an item 1 unit in xz plane
    private void MoveLeft(Transform pos)
    {
        pos.Translate(Vector3.left);
    }

    private void MoveRight(Transform pos)
    {
        pos.Translate(Vector3.right);
    }

    private void MoveUp(Transform pos)
    {
        pos.Translate(Vector3.forward);
    }

    private void MoveDown(Transform pos)
    {
        pos.Translate(Vector3.back);
    }
    
    
}
