using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Level
{
	public class Door : MonoBehaviour
	{
		[SerializeField] private bool closed;
		[SerializeField] private GameObject closedDoorRenderer, openedDoorRenderer;

		private ILogger _logger;
		
		public bool Closed => closed;

		[Inject]
		public void Construct(ILogger logger)
		{
			_logger = logger;
		}
		
		private void UpdateModels()
		{
			closedDoorRenderer.gameObject.SetActive(Closed);
			openedDoorRenderer.gameObject.SetActive(!Closed);
		}

		public void SwitchDoorState()
		{
			_logger.Log(this,$"Switching Door {gameObject.name} State");

			if (Closed)
				OpenTheDoor();
			else
				CloseTheDoor();
			
			_logger.Log(this,$"Done switching Door {gameObject.name} State");

		}

		private void OpenTheDoor()
		{
			_logger.Log(this,$"Door {gameObject.name} has been opened");
			closed = false;
			UpdateModels();
		}

		private void CloseTheDoor()
		{
			_logger.Log(this,$"Door {gameObject.name} has been closed");
			closed = true;
			UpdateModels();
		}
	}
}
