using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
   
    public Transform SpawnPoint;
    private GameController gameController;
    private Player_controller PlayerController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        /*GameObject PlayerControllerObject = GameObject.FindWithTag("Player_controller");
        if (PlayerControllerObject != null)
        {
            PlayerController = PlayerControllerObject.GetComponent<Player_controller>();
        }*/

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            gameController.playerState = GameController.PlayerState.Dead;        
        }

        
    }
}
