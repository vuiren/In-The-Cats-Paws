using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer
{
    public class NetworkPrefabFactory: IFactory<string, Vector3, bool, GameObject>, IFactory<string, GameObject>
    {
        [Inject] private DiContainer _container = null;
        private IMyLogger _myLogger;

        [Inject]
        public NetworkPrefabFactory()
        {
            _myLogger = LoggerFactory.Create(this);
        }

        public GameObject Create(string prefabName, Vector3 spawnPoint, bool injectDependencies = true)
        {
            var gameObject =
                PhotonNetwork.Instantiate(prefabName,
                    spawnPoint, new Quaternion());
            _myLogger.Log( $"Network Instantiated prefab {prefabName} at {spawnPoint}");

            if (injectDependencies)
                _container.InjectGameObject(gameObject);
            return gameObject;
        }

        public GameObject Create(string prefabName)
        {
            var gameObject = 
                PhotonNetwork.Instantiate(prefabName, 
                    new Vector3(), new Quaternion());
            _myLogger.Log($"Network Instantiated prefab {prefabName}");
            
            _container.InjectGameObject(gameObject);
            return gameObject;
        }
    }
}