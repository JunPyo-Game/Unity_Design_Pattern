using System.Collections;
using UnityEngine;
enum ButtonColor
{
    Red,
    Blue
}

public class StepButton : MonoBehaviour, IButtonInteractable
{
    [SerializeField] private float pressDepth = 1.0f;
    [SerializeField] private float pressSpeed = 2.0f;
    [SerializeField] private bool autoRelease = true;
    [SerializeField] private ButtonColor color;

    // public UnityEvent PresseButtonEvent = new();
    // public UnityEvent ReleaseButtonEvent = new();

    private float curDepth = 0.0f;

    private Coroutine pressRoutin;
    private Coroutine releaseRoutin;

    private IEnumerator PressRoutine(Transform buttonTransform)
    {
        while (pressDepth >= curDepth)
        {
            float deltaDepth = GetDeltaDepth();
            buttonTransform.position -= Vector3.up * deltaDepth;
            curDepth += deltaDepth;

            yield return null;
        }

        if (pressDepth < curDepth)
        {
            float diff = curDepth - pressDepth;
            buttonTransform.position += new Vector3(0.0f, diff, 0.0f);
            curDepth = pressDepth;
        }
    }

    private IEnumerator ReleaseRoutine(Transform buttonTransform)
    {
        while (curDepth >= 0.0f)
        {
            float deltaDepth = GetDeltaDepth();
            buttonTransform.position += Vector3.up * deltaDepth;
            curDepth -= deltaDepth;

            yield return null;
        }

        if (curDepth < 0.0f)
        {
            buttonTransform.position -= new Vector3(0.0f, curDepth, 0.0f);
            curDepth = 0.0f;
        }
    }

    private float GetDeltaDepth()
    {
        return pressSpeed * pressDepth * Time.deltaTime;
    }

    public void OnButtonPressed(Button button, Collider other)
    {
        if (releaseRoutin != null)
        {
            StopCoroutine(releaseRoutin);
            releaseRoutin = null;
        }

        pressRoutin = StartCoroutine(PressRoutine(button.transform));

        if (color == ButtonColor.Red)
            EventManager.Instance.Trigger("OnRedButtonPress");
        else
            EventManager.Instance.Trigger("OnBlueButtonPress");
    }

    public void OnButtonReleased(Button button, Collider other)
    {
        if (pressRoutin != null)
        {
            StopCoroutine(pressRoutin);
            pressRoutin = null;
        }

        releaseRoutin = StartCoroutine(ReleaseRoutine(button.transform));

        if (color == ButtonColor.Red)
            EventManager.Instance.Trigger("OnRedButtonRelease");
        else
            EventManager.Instance.Trigger("OnBlueButtonRelease");
    }
}
