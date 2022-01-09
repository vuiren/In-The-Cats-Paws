using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Input
{
	public abstract class PlayerInput : MonoBehaviour
	{
		protected IInputService InputService;
		[SerializeField]
		protected Camera Camera;
		
		[Inject]
		public virtual void Construct(IInputService inputService)
		{
			InputService = inputService;
			Camera = Camera.main;
		}
		
		protected void Update()
		{
			ProcessInput();
		}

		protected abstract void ProcessInput();
	}
}