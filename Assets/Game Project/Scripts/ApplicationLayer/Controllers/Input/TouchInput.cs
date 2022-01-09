using UnityEngine;
using UnityEngine.EventSystems;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Input
{
	public sealed class TouchInput : PlayerInput
	{
		private void Start()
		{
			if (Application.platform != RuntimePlatform.Android)
			{
				Destroy(this);
			}
		}

		protected override void ProcessInput()
		{
			if (UnityEngine.Input.touchCount <= 0) return;
			var touch = UnityEngine.Input.GetTouch(0);

			if (touch.phase != TouchPhase.Began || EventSystem.current.IsPointerOverGameObject(touch.fingerId)) return;
			
			var position = Camera.ScreenToWorldPoint(touch.position);
			InputService.PlayerClick(position);
		}
	}
}
