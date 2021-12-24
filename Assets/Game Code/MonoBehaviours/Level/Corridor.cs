using System.Collections.Generic;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Level
{
	public class Corridor : MonoBehaviour
	{
		[SerializeField] private Room[] connectedRooms;
		[SerializeField] private Door door;
		
		public IEnumerable<Room> ConnectedRooms => connectedRooms;
		public Door Door => door;

		private void OnDrawGizmos()
		{
			if (connectedRooms.Length == 0) return;

			var prevPos = connectedRooms[0].transform.position;
			foreach(var e in connectedRooms)
			{
				Gizmos.color = Color.black;
				var position = e.transform.position;
				Gizmos.DrawLine(prevPos, position);
				prevPos = position;
			}
		}
	}
}
