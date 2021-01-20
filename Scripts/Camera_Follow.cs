using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform player;
    public Vector3 Offset;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValue;


    void LateUpdate()
    { 
        Follow();        
    }
    void Follow()
    {
        Vector3 playerPosition = player.position + Offset;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(playerPosition.x, minValues.x, maxValue.x),
            Mathf.Clamp(playerPosition.y, minValues.y, maxValue.y),
            Mathf.Clamp(playerPosition.z, minValues.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition,smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
