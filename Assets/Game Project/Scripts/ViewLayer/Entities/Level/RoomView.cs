using System.Collections.Generic;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
	public class RoomView : Entity<Room>
	{
		[SerializeField] private Vector2Int roomCoords;
		[SerializeField] private Vector2Int[] exits;
		[SerializeField] private bool isShadowRoom;
		[SerializeField] private Transform pointsForUnitsParent;

		public Vector2Int[] Exits => exits;

		protected override void SetModel()
		{
			model.Coords = roomCoords;
			model.ShadowRoom = isShadowRoom;

			var result = new List<Vector3>();
			for (int i = 0; i < pointsForUnitsParent.childCount; i++)
			{
				var child = pointsForUnitsParent.GetChild(i).transform.position;
				result.Add(child);
			}

			model.FreePoints = result;
		}

		private void OnDrawGizmosSelected()
		{
			foreach (var connectedRoom in exits)
			{
				Gizmos.DrawLine(transform.position,
					new Vector3(connectedRoom.x, connectedRoom.y, transform.position.z));
			}
		}
	}
}
