using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer
{
	[RequireComponent(typeof(Camera))]
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Transform unZoomPosition;
		[SerializeField] private float zoomSize, unZoomSize, zoomSpeed, moveSpeed;


		private IUnitsSelectionService _selectionService;
		private Camera _camera;
		public bool zoom;

		[Inject]
		public void Construct(IUnitsSelectionService selectionService)
		{
			_selectionService = selectionService;
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

			var selectedUnit = _selectionService.GetSelectedUnit();
			if (selectedUnit != null && zoom)
			{
				var position = _camera.transform.position;
				var cameraPos = position;
				var targetPos = selectedUnit.GameObjectLink.transform.position;
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