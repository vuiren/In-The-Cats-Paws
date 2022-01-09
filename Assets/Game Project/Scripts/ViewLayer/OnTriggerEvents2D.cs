using System;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer
{
	public class OnTriggerEvents2D : MonoBehaviour
	{
		public Action<Collider2D> OnTriggerEnter { get; set; }
		public Action<Collider2D> OnTriggerExit { get; set; }

		private void OnTriggerEnter2D(Collider2D collision)
		{
			OnTriggerEnter?.Invoke(collision);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			OnTriggerExit?.Invoke(collision);
		}
	}
}
