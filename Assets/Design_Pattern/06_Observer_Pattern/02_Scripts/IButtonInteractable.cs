using UnityEngine;

public interface IButtonInteractable
{
    void OnButtonPressed(Button button, Collider other);
    void OnButtonReleased(Button button, Collider other);
}