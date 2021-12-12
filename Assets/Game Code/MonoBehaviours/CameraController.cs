using Game_Code.MonoBehaviours.Level;
using Game_Code.MonoBehaviours.Units;
using UnityEngine;

namespace Game_Code.MonoBehaviours
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform unZoomPosition;
		[SerializeField] private float zoomSize, unZoomSize, zoomSpeed, moveSpeed;

		private Camera _camera;

		public Unit selectedUnit;
		public Room selectedRoom;
		public bool zoom;

		private void Awake()
		{
			_camera = GetComponent<Camera>();
		}

		public void ToggleZoom() => zoom = !zoom;

		private void LateUpdate()
		{
			_camera.orthographicSize = 
				Mathf.MoveTowards(
					_camera.orthographicSize, 
					zoom ? zoomSize : unZoomSize,
					zoomSpeed * Time.deltaTime);

			if (selectedUnit && zoom)
			{
				var position = _camera.transform.position;
				var cameraPos = position;
				var targetPos = selectedUnit.transform.position;
				targetPos.z = cameraPos.z;

				position = Vector3.MoveTowards(position, targetPos, moveSpeed * Time.deltaTime);
				_camera.transform.position = position;
			}
			else
			{
				var position = _camera.transform.position;
				var cameraPos = position;
				var targetPos = unZoomPosition.position;
				targetPos.z = cameraPos.z;

				position = Vector3.MoveTowards(position, targetPos, moveSpeed * Time.deltaTime);
				_camera.transform.position = position;
			}
		}
	}
}