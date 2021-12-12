using UnityEngine;
using UnityEngine.EventSystems;

namespace Game_Code.MonoBehaviours.Input
{
	public class TouchInput : PlayerInput
	{
		protected override void ProcessInput()
		{
			if (UnityEngine.Input.touchCount <= 0) return;
			var touch = UnityEngine.Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
			{
				Player.MakeStep(touch.position);
			}
		}
	}
}
