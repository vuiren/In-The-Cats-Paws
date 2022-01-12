using System.Collections.Generic;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Entities.Level
{
	public class RoomView : Entity<Room>
	{
		[SerializeField] private Vector2Int roomCoords;
		[SerializeField] private bool isShadowRoom;
		[SerializeField] private GameObject cover, coverForEngineer;
		[SerializeField] private Transform pointsForUnitsParent;
		[SerializeField] private int roomId;


		protected override void SetModel()
		{
			roomId = transform.GetSiblingIndex();
			model = new Room
			{
				Coords = roomCoords,
				ShadowRoom = isShadowRoom,
				ID = transform.GetSiblingIndex(),
			};

			var result = new List<Vector3>();
			for (int i = 0; i < pointsForUnitsParent.childCount; i++)
			{
				var child = pointsForUnitsParent.GetChild(i).transform.position;
				result.Add(child);
			}

			model.FreePoints = result;
		}

		public void Draw(bool draw)
		{
			cover.SetActive(!draw);
		}

		public void DrawForEngineer()
		{
			cover.SetActive(true);
			coverForEngineer.SetActive(true);
		}
		
		#if UNITY_EDITOR 
		private void OnDrawGizmos()
		{
			var style = new GUIStyle();
			style.normal.textColor = Color.green;
			
			Handles.Label(transform.position, roomCoords.ToString(), style);
		}
		#endif
	}
}
