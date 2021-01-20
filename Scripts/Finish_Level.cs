using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Level : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            Debug.Log("it works");
            
        }


    }
}
