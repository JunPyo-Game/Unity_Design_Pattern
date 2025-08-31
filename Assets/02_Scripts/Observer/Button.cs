using UnityEngine;

public class Button : MonoBehaviour
{
    private IButtonInteractable buttonParent;

    private void Awake()
    {
        buttonParent = GetComponentInParent<IButtonInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        buttonParent?.OnButtonPressed(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        buttonParent?.OnButtonReleased(this, other);
    }
}