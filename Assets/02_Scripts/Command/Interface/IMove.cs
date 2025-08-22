using UnityEngine;

public interface IMove
{
    public void Move(Vector3 movement);

    public bool IsValidMove(Vector3 movement);
}
