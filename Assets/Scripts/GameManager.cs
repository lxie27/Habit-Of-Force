using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerCursor;
    public bool playerTurn;
    PlayerControl playerControl;

    void Start()
    {
        playerControl = playerCursor.GetComponent<PlayerControl>();
        playerTurn = playerControl.playerTurn;
    }

    void Update()
    {
        playerTurn = playerControl.playerTurn;

    }
}
