using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    interface IMovement
    {
        Vector2 GetMovement(int index);
    }

    class GamepadMovement : IMovement
    {
        public Vector2 GetMovement(int index)
        {
            if(index == 0)
            {
                return new Vector2(Input.GetAxis("Horizontal0"), Input.GetAxis("Vertical0"));
            }
            return new Vector2(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"));
        }
    }

    class KeyboardMovement : IMovement
    {
        public Vector2 GetMovement(int index)
        {
            Vector2 movementVector = Vector2.zero;
            if(index == 0)
            {
                if (Input.GetKey(KeyCode.W))
                    movementVector.y += 1;
                if (Input.GetKey(KeyCode.S))
                    movementVector.y -= 1;
                if (Input.GetKey(KeyCode.A))
                    movementVector.x -= 1;
                if (Input.GetKey(KeyCode.D))
                    movementVector.x += 1;
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                    movementVector.y += 1;
                if (Input.GetKey(KeyCode.DownArrow))
                    movementVector.y -= 1;
                if (Input.GetKey(KeyCode.LeftArrow))
                    movementVector.x -= 1;
                if (Input.GetKey(KeyCode.RightArrow))
                    movementVector.x += 1;
            }
            
            return movementVector;
        }
    }
}
