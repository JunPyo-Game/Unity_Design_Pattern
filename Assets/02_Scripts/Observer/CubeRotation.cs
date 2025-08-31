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
            EventManager.Instance.Subscribe("OnRedButtonPress", HandleButtenPressed);
            EventManager.Instance.Subscribe("OnRedButtonRelease", HandleButtenReleased);
        }
        else
        {
            EventManager.Instance.Subscribe("OnBlueButtonPress", HandleButtenPressed);
            EventManager.Instance.Subscribe("OnBlueButtonRelease", HandleButtenReleased);
        }
    }

    private void OnDisable()
    {
        if (color == CubeColor.Red)
        {
            EventManager.Instance.Unsubscribe("OnRedButtonPress", HandleButtenPressed);
            EventManager.Instance.Unsubscribe("OnRedButtonRelease", HandleButtenReleased);
        }
        else
        {
            EventManager.Instance.Unsubscribe("OnBlueButtonPress", HandleButtenPressed);
            EventManager.Instance.Unsubscribe("OnBlueButtonRelease", HandleButtenReleased);
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
