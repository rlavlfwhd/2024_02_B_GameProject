using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CommandManager CommandManager;
    
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            ICommand moveRight = new MoveCommand(transform, Vector3.right);
            CommandManager.ExcuteCommand(moveRight);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ICommand moveLeft = new MoveCommand(transform, Vector3.left);
            CommandManager.ExcuteCommand(moveLeft);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {            
            CommandManager.UndoLastCommand();
        }
    }
}
