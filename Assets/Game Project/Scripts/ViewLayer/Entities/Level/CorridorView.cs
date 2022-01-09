using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
	public sealed class CorridorView : Entity<Corridor>
	{
		[SerializeField] private Vector2Int room1, room2;
		[SerializeField] private GameObject closedCorridorRenderer, openedCorridorRenderer;
		
		protected override void SetModel()
		{
			model.Room1 = room1;
			model.Room2 = room2;
		}

		public void DrawCorridor(bool draw)
		{
			if (draw)
			{
				openedCorridorRenderer.SetActive(!model.Locked);
				closedCorridorRenderer.SetActive(model.Locked);
			}
			else
			{
				openedCorridorRenderer.SetActive(true);
				closedCorridorRenderer.SetActive(false);
			}
		}
		
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(new Vector3(room1.x, room1.y), new Vector3(room2.x, room2.y));
		}
	}
}
