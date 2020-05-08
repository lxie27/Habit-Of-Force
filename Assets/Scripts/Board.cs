using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This class defines the *virtual* game board that contains the data for squares
 *  and units in those squares.
 */
public class Board
{
    public int boardSize;       //Length of a board's side (ex. 8x8 board would be size 8)
    public Square[,] squares;   //The individual squares of this board

    //constructor for an empty board given a size
    public Board()
    {
        Square[,] emptyBoard = new Square[8, 8];

        // Populating the board
        for (int i = 0; i < 8 * 8; i++)
        {
            emptyBoard[i / 8, i % 8] = new Square();
        }

        boardSize = 8;
        squares = emptyBoard;
    }

    //constructor for an empty board given a size
    public Board(int size)
    {
        Square[,] emptyBoard = new Square[size, size];

        // Populating the board
        for (int i = 0; i < size * size; i++)
        {
            emptyBoard[i / size, i % size] = new Square();
        }

        boardSize = size;
        squares = emptyBoard;
    }

    //TESTING BOARD
    public Board generateTestBoard(int boardSize)
    {
        Board testBoard = new Board(boardSize);
        
        Unit ally1 = new Unit(Affiliation.ally);
        Unit enemy1 = new Unit(Affiliation.enemy);
        
        testBoard.squares[3, 3].addUnit(ally1);
        testBoard.squares[2, 2].addUnit(enemy1);
        
        ally1.model = GameObject.Instantiate(ally1.model, new Vector3(3, 0, 3), Quaternion.identity);
        enemy1.model = GameObject.Instantiate(enemy1.model, new Vector3(2, 0, 2), enemy1.model.transform.rotation);

        return testBoard;
    }

    // Job of caller to remove item from original board spot
    // pos - coordinates of the item's destination
    // item - usually the unit being moved
    public void moveItem(Vector3 pos, Unit item)
    {
        if (this.squares[(int)pos.x, (int)pos.z].isEmpty)
        {
            this.squares[(int)pos.x, (int)pos.z].unit = item;
        }
    }

    public bool squareHasItem(Vector3 pos)
    {
        if (!this.squares[(int)pos.x, (int)pos.z].isEmpty)
        {
            return true;
        }
        return false;
    }

    public bool removeItem(Vector3 pos)
    {
        if (!this.squares[(int)pos.x, (int)pos.z].isEmpty)
        {
            this.squares[(int)pos.x, (int)pos.z].unit = null;
            this.squares[(int)pos.x, (int)pos.z].isEmpty = true;
            return true;
        }
        return false;
    }
    void moveItemLeft(Unit item)
    {
        int x = (int)item.position.x;
        int y = (int)item.position.y;

        if (x > 0)
        {
            if (this.squares[x - 1, y].isEmpty)
            {
                this.squares[x - 1, y].unit = item;
                this.squares[x, y].unit = null;
            }
        }
    }
    void moveItemRight(Unit item)
    {
        int x = (int)item.position.x;
        int y = (int)item.position.y;

        if (x < this.boardSize)
        {
            if (this.squares[x + 1, y].isEmpty)
            {
                this.squares[x + 1, y].unit = item;
                this.squares[x, y].unit = null;
            }
        }
    }
    void moveItemUp(Unit item)
    {
        int x = (int)item.position.x;
        int y = (int)item.position.y;

        if (y > 0)
        {
            if (this.squares[x, y + 1].isEmpty)
            {
                this.squares[x, y + 1].unit = item;
                this.squares[x, y].unit = null;
            }
        }
    }
    void moveItemDown(Unit item)
    {
        int x = (int)item.position.x;
        int y = (int)item.position.y;

        if (y < this.boardSize)
        {
            if (this.squares[x, y - 1].isEmpty)
            {
                this.squares[x, y - 1].unit = item;
                this.squares[x, y].unit = null;
            }
        }
    }
}
