using Game_Code.MonoBehaviours.Level;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network
{
    public class NetworkPrefabFactory: IFactory<string, SpawnPoint, GameObject>, IFactory<string, GameObject>
    {
        [Inject] private DiContainer _container = null;

        [Inject] private ILogger _logger;
        
        public GameObject Create(string prefabName, SpawnPoint spawnPoint)
        {
            var gameObject = 
                PhotonNetwork.Instantiate(prefabName, 
                    spawnPoint.SpawnPointTransform.position, new Quaternion());
            _logger.Log(this,$"Network Instantiated prefab {prefabName} at {spawnPoint.name}");
            
            _container.InjectGameObject(gameObject);
            return gameObject;
        }
        
        public GameObject Create(string prefabName)
        {
            var gameObject = 
                PhotonNetwork.Instantiate(prefabName, 
                    new Vector3(), new Quaternion());
            _logger.Log(this,$"Network Instantiated prefab {prefabName}");
            
            _container.InjectGameObject(gameObject);
            return gameObject;
        }
    }
}