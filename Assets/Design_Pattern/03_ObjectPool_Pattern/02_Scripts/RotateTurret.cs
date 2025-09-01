using UnityEngine;

public class RotateTurret : MonoBehaviour
{
    [SerializeField] private Transform turretHead;
    [SerializeField] private float sensitivity;

    private void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Camera.main.WorldToScreenPoint(turretHead.position).z;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);

        mouseWorld.y = turretHead.position.y;
        turretHead.LookAt(mouseWorld);
    }
}
