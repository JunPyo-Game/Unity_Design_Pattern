using UnityEngine;
using UnityEngine.UI;

public class UICountUpdate : MonoBehaviour
{
    [SerializeField] private Text allCountText;
    [SerializeField] private Text InActiveText;
    [SerializeField] private Text ActiveText;
    [SerializeField] private TurretFire turretFire;

    private void Update()
    {
        turretFire.GetCount(out int allCount, out int inActiveCount, out int activeCount);

        allCountText.text = $"All Bullet Count : {allCount}";
        InActiveText.text = $"Inactive Bullet Count : {inActiveCount}";
        ActiveText.text = $"Active Bullet Count : {activeCount}";
    }
}
