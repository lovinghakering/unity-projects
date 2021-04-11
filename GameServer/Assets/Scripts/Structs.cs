using UnityEngine;

public struct Inputs
{
    public Inputs(bool _moveForward, bool _moveBackward, bool _moveLeft, bool _moveRight, bool _jump)
    {
        MoveForward = _moveForward;
        MoveBackward = _moveBackward;
        MoveLeft = _moveLeft;
        MoveRight = _moveRight;
        Jump = _jump;
    }

    public bool MoveForward { get; }
    public bool MoveBackward { get; }
    public bool MoveLeft { get; }
    public bool MoveRight { get; }
    public bool Jump { get; }
}