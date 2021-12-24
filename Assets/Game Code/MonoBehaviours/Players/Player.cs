using System;
using Game_Code.Factories;
using Game_Code.MonoBehaviours.Data;
using Game_Code.Services;
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
        protected UnitFactory UnitSpawnManager;
        protected StaticData StaticData;
        protected ILogger Logger;
        protected IUnitsSelectionService SelectionService;

        public Action OnStepMade { get; set; }

        [Inject]
        public void Construct(ILogger logger, StaticData staticData, SceneData sceneData, 
            UnitFactory unitFactory, IUnitsSelectionService selectionService)
        {
            Logger = logger;
            Logger.Log(this,$"Player {gameObject.name} has been initialized");
            CameraController = sceneData.cameraController;
            UnitSpawnManager = unitFactory;
            StaticData = staticData;
            SelectionService = selectionService;
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