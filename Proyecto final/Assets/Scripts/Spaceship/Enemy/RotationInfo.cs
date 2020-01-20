using UnityEngine;

public class RotationInfo
{
    public enum Direction
    {
        Left,
        Right
    }

    public Vector3 PreviousAsteroidPosition { get; set; }
    public Vector3 CurrentAsteroidPosition { get; set; }

    public Direction CurrentDirection { get; set; }
    public Quaternion SpaceshipRotation { get; set; }

    public RotationInfo()
    {
        PreviousAsteroidPosition = new Vector3(0, 0, 0);
        CurrentAsteroidPosition = new Vector3(0, 0, 0);
        CurrentDirection = Direction.Left;
    }
}
