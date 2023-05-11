using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPosition; //gets the position of cameraPosition

    void Update()
    {
        transform.position = cameraPosition.position; //Sets the position of the camera holder to the comera position child of 'Player'
    }
}