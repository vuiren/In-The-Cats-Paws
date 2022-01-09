using UnityEngine.EventSystems;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Input
{
    public class MouseInput : PlayerInput
    {
        protected override void ProcessInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var position = UnityEngine.Input.mousePosition;
                var worldPoint = Camera.ScreenToWorldPoint(position);
                InputService.PlayerClick(worldPoint);
            }
        }
    }
}
