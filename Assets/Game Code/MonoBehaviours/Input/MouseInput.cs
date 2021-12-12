using UnityEngine.EventSystems;

namespace Game_Code.MonoBehaviours.Input
{
    public class MouseInput : PlayerInput
    {
        protected override void ProcessInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Player.MakeStep(UnityEngine.Input.mousePosition);
            }
        }
    }
}
