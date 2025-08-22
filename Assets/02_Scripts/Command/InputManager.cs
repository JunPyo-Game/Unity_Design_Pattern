using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerMove player;

    private readonly Vector3 forward = new(0, 0, 2);
    private readonly Vector3 back = new(0, 0, -2);
    private readonly Vector3 right = new(2, 0, 0);
    private readonly Vector3 left = new(-2, 0, 0);

    private void Awake()
    {
        player = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            RunPlayerCommand(forward);

        if (Input.GetKeyDown(KeyCode.A))
            RunPlayerCommand(left);

        if (Input.GetKeyDown(KeyCode.S))
            RunPlayerCommand(back);

        if (Input.GetKeyDown(KeyCode.D))
            RunPlayerCommand(right);

        if (Input.GetKeyDown(KeyCode.C))
            CommandInvoker.Instance.Undo();

        if (Input.GetKeyDown(KeyCode.V))
            CommandInvoker.Instance.Redo();

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reset();
            CommandInvoker.Instance.Replay();
        }
    }

    private void RunPlayerCommand(Vector3 movement)
    {
        if (player.IsValidMove(movement))
        {
            ICommand command = new MoveCommand(player, movement);
            CommandInvoker.Instance.Execute(command);
        }
    }
}
