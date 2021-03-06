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

//public struct Cordinates
//{
//    public Cordinates(float _xPos, float _yPos, float _zPos, float _xRot, float _yRot, float _zRot, float _wRot)
//    {
//        xPos = _xPos;
//        yPos = _yPos;
//        zPos = _zPos;

//        xRot = _xRot;
//        yRot = _yRot;
//        zRot = _zRot;
//        wRot = _wRot;
//    }

//    //Position
//    public float xPos { get; }
//    public float yPos { get; }
//    public float zPos { get; }

//    public Vector3 GetPosition()
//    {
//        return new Vector3(xPos, yPos, zPos);
//    }

//    //Rotation
//    public float xRot { get; }
//    public float yRot { get; }
//    public float zRot { get; }
//    public float wRot { get; }

//    public Quaternion GetRotation()
//    {
//        return new Quaternion(xRot, yRot, zRot, wRot);
//    }
//}