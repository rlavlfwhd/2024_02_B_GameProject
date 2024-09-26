using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Excute();
    void Undo();
}
public class MoveCommand : ICommand
{
    private Transform ObjectToMove;
    private Vector3 displacement;

    public MoveCommand(Transform obj, Vector3 displacement)
    {
        this.ObjectToMove = obj;
        this.displacement = displacement;
    }

    public void Excute() { ObjectToMove.position += displacement; }
    public void Undo() {  ObjectToMove.position -= displacement; }
}