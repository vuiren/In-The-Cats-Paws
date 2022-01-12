using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.UI
{
    public sealed class ZoomButton : MonoBehaviour
    {
        [Inject] private CameraController _cameraController;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Zoom);
        }

        private void Zoom()
        {
            _cameraController.zoom = !_cameraController.zoom;
        }
    }
}