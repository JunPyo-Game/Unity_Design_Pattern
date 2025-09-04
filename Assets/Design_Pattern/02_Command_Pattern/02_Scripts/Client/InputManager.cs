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
        player = FindAnyObjectByType<PlayerMove>();
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
            CommandInvoker.Undo();

        if (Input.GetKeyDown(KeyCode.V))
            CommandInvoker.Redo();

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reset();
            ReplayManager.Instance.Replay(CommandInvoker.CommandList);
        }
    }

    private void RunPlayerCommand(Vector3 movement)
    {
        Command command = new MoveCommand(player, movement);
        CommandInvoker.Execute(command);
    }
}
