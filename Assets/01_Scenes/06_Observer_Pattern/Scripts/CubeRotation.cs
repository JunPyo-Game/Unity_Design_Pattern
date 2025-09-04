using UnityEngine;

enum CubeColor
{
    Red, 
    Blue,
}

public class CubeRotation : MonoBehaviour
{
    [SerializeField] private CubeColor color;
    private float rotateSpeed = 0.0f;

    private void OnEnable()
    {
        if (color == CubeColor.Red)
        {
            EventBus.Subscribe("OnRedButtonPress", HandleButtenPressed);
            EventBus.Subscribe("OnRedButtonRelease", HandleButtenReleased);
        }
        else
        {
            EventBus.Subscribe("OnBlueButtonPress", HandleButtenPressed);
            EventBus.Subscribe("OnBlueButtonRelease", HandleButtenReleased);
        }
    }

    private void OnDisable()
    {
        if (color == CubeColor.Red)
        {
            EventBus.Unsubscribe("OnRedButtonPress", HandleButtenPressed);
            EventBus.Unsubscribe("OnRedButtonRelease", HandleButtenReleased);
        }
        else
        {
            EventBus.Unsubscribe("OnBlueButtonPress", HandleButtenPressed);
            EventBus.Unsubscribe("OnBlueButtonRelease", HandleButtenReleased);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(rotateSpeed, rotateSpeed, 0) * Time.deltaTime);
    }

    private void HandleButtenPressed()
    {
        rotateSpeed = 60.0f;
    }

    private void HandleButtenReleased()
    {
        rotateSpeed = 0.0f;
    }
}
