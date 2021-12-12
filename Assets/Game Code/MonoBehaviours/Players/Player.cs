using System;
using Game_Code.Controllers;
using Game_Code.Factories;
using Game_Code.Managers;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Units;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.MonoBehaviours.Players
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class Player : MonoBehaviour
    {
        [SerializeField] protected LayerMask roomLayers, unitsLayers;

        protected PhotonView PhotonView;
        protected Camera Camera;
        protected CameraController CameraController;
        protected Unit ControlledUnit;
        protected UnitFactory UnitSpawnManager;
        protected StaticData StaticData;
        protected ILogger Logger;

        public Action OnStepMade { get; set; }

        [Inject]
        public void Construct(ILogger logger,
            StaticData staticData, SceneData sceneData, UnitFactory unitFactory)
        {
            Logger = logger;
            Logger.Log("Player has been initialized");
            CameraController = sceneData.cameraController;
            UnitSpawnManager = unitFactory;
            StaticData = staticData;
        }

        protected virtual void Awake()
        {
            Camera = Camera.main;
            PhotonView = GetComponent<PhotonView>();
        }

        protected abstract void SpawnControllableUnits();
        public abstract void MakeStep(Vector3 cursorPos);

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (!PhotonView.IsMine) return;
            SpawnControllableUnits();
        }

        protected T RaycastForComponent<T>(Vector3 mousePos, LayerMask layers) where T : MonoBehaviour
        {
            var hit = Physics2D.Raycast(mousePos, Camera.transform.forward, 100, layers);
            if (!hit) return null;
            var component = hit.transform.parent.GetComponent<T>();

            return component ? component : null;
        }

    }
}