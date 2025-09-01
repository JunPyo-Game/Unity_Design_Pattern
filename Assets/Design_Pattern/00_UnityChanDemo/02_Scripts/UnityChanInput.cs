
using UnityEngine;

namespace UnityChanDemo
{
    public class UnityChanInput : MonoBehaviour
    {
        public static float GetVertical()
        {
            return Input.GetAxisRaw("Vertical");
        }

        public static float GetHorizontal()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public static bool GetToggleRunModeKey()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }

        public static bool GetJumpKey()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public static bool GetSlideKey()
        {
            return Input.GetKeyDown(KeyCode.C);
        }

        public static bool GetUmatobiKey()
        {
            return Input.GetKeyDown(KeyCode.F);
        }
    }
}


