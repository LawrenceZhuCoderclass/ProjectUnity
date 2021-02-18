using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    public Transform SpawnPoint;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("it works");
            col.transform.position = SpawnPoint.position;
        }

        
    }
}
