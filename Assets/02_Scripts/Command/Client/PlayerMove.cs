using UnityEngine;

public class PlayerMove : MonoBehaviour, IMove
{
    [SerializeField] private LayerMask obstacleLayer;
    private const float boardSpacing = 1.0f;
    private Vector3 oriPos;

    private void Start()
    {
        oriPos = transform.position;
    }

    public void Reset()
    {
        transform.position = oriPos;
    }

    public void Move(Vector3 movement)
    {
        transform.position += movement;
    }

    public bool IsValidMove(Vector3 movement)
    {
        return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
    }
}
