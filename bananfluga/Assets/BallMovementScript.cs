﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;

public class BallMovementScript : MonoBehaviour
{

    Rigidbody rigidBody;
    public int Index = 0;
    public bool UseGamepad = false;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var maxForce = 10;
        var movement = UseGamepad ?
            new GamepadMovement().GetMovement(Index) : 
            new KeyboardMovement().GetMovement(Index);

        rigidBody.AddForce(movement.x * maxForce, 0,  movement.y * maxForce);

        //if (Input.GetKey(KeyCode.W))
        //    rigidBody.AddForce(new Vector3(0, 0, maxForce));
        //if (Input.GetKey(KeyCode.S))
        //    rigidBody.AddForce(new Vector3(0, 0, -maxForce));
        //if (Input.GetKey(KeyCode.A))
        //    rigidBody.AddForce(new Vector3(-maxForce, 0, 0));
        //if (Input.GetKey(KeyCode.D))
        //    rigidBody.AddForce(new Vector3(maxForce, 0, 0));
    }
}