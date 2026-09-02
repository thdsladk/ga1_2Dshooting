using System;
using UnityEngine;
using System.Collections.Generic;

public class KeyCommand : MonoBehaviour
{
    protected List<KeyCode> _commands;

    KeyCommand()
    {
        _commands = new List<KeyCode>();
    }
     private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _commands.Add(KeyCode.W);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _commands.Add(KeyCode.D);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _commands.Add(KeyCode.A);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _commands.Add(KeyCode.S);
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            _commands.Add(KeyCode.E);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _commands.Add(KeyCode.Q);
        }
    }


    public List<KeyCode> StartReplay()
    {
        return _commands;
    }


    public void MoveCharacter(KeyCode key)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _commands.Add(KeyCode.W);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _commands.Add(KeyCode.D);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _commands.Add(KeyCode.A);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            _commands.Add(KeyCode.S);
        }
    }
}
