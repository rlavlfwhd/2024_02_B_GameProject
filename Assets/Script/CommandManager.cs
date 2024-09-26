using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> commandHistory = new Stack<ICommand>();

    public void ExcuteCommand(ICommand command)
    {
        command.Excute();
        commandHistory.Push(command);
    }

    public void UndoLastCommand()
    {
        if(commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory.Pop();
            lastCommand.Undo();
        }
    }
}
