using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
	public sealed class CorridorView : Entity<Corridor>
	{
		[SerializeField] private Vector2Int room1, room2;
		[SerializeField] private bool isShadowCorridor;
		[SerializeField] private GameObject corridorCover;
		[SerializeField] private GameObject closedCorridorRenderer, openedCorridorRenderer;
		[SerializeField] private Vector3 debugTextOffset;
		
		protected override void SetModel()
		{
			model = new Corridor
			{
				Room1 = room1,
				Room2 = room2,
				ShadowCorridor = isShadowCorridor
			};
		}

		public void DrawCorridor(bool draw)
		{
			if (draw)
			{
				openedCorridorRenderer.SetActive(!model.Locked);
				closedCorridorRenderer.SetActive(model.Locked);
				corridorCover.SetActive(false);
			}
			else
			{
				openedCorridorRenderer.SetActive(true);
				closedCorridorRenderer.SetActive(false);
				corridorCover.SetActive(true);
			}
		}
		
		#if UNITY_EDITOR 
		private void OnDrawGizmos()
		{
			var style = new GUIStyle();
			style.normal.textColor = Color.red;
			
			Handles.Label(transform.position + debugTextOffset, $"{room1} to {room2}", style);
		}
		#endif
	}
}
