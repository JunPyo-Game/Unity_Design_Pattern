using System.Collections;
using UnityEngine;

enum DoorColor
{
    Red,
    Blue
}

[RequireComponent(typeof(MeshRenderer))]
public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorColor color;

    private float moveDist;
    private float curMoveDist = 0.0f;

    private Coroutine openRoutine;
    private Coroutine closeRoutine;
    

    private void Awake()
    {
        moveDist = transform.localScale.y;
    }

    private void OnEnable()
    {
        if (color == DoorColor.Red)
        {
            EventBus.Subscribe("OnRedButtonPress", HandleDoorOpen);
            EventBus.Subscribe("OnRedButtonRelease", HandleDoorClose);
        }
        else
        {
            EventBus.Subscribe("OnBlueButtonPress", HandleDoorOpen);
            EventBus.Subscribe("OnBlueButtonRelease", HandleDoorClose);
        }
    }

    private void OnDisable()
    {
        if (color == DoorColor.Red)
        {
            EventBus.Unsubscribe("OnRedButtonPress", HandleDoorOpen);
            EventBus.Unsubscribe("OnRedButtonRelease", HandleDoorClose);
        }
        else
        {
            EventBus.Unsubscribe("OnBlueButtonPress", HandleDoorOpen);
            EventBus.Unsubscribe("OnBlueButtonRelease", HandleDoorClose);
        }
    }

    public void HandleDoorOpen()
    {
        if (closeRoutine != null)
        {
            StopCoroutine(closeRoutine);
            closeRoutine = null;
        }

        openRoutine = StartCoroutine(OpenRoutine());
    }

    public void HandleDoorClose()
    {
        if (openRoutine != null)
        {
            StopCoroutine(openRoutine);
            openRoutine = null;
        }

        closeRoutine = StartCoroutine(ClostRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        while (curMoveDist <= moveDist)
        {
            float dist = Time.deltaTime * moveDist;
            transform.position += Vector3.up * dist;
            curMoveDist += dist;

            yield return null;
        }
    }

    private IEnumerator ClostRoutine()
    {
        while (curMoveDist >= 0)
        {
            float dist = Time.deltaTime * moveDist;
            transform.position -= Vector3.up * dist;
            curMoveDist -= dist;

            yield return null;
        }
    }
}
