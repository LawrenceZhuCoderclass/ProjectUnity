﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // dit zorgt ervoor dat de camera de speler volgt
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
