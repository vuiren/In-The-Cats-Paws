using Game_Code.MonoBehaviours.Input;
using Game_Code.MonoBehaviours.Players;
using Game_Code.Network;
using Game_Code.Services;
using UnityEngine;

namespace Game_Code.Factories
{
    public class PlayerFactory
    {
        private readonly NetworkPrefabFactory _networkPrefabFactory;
        private readonly ITurnService _turnService;

        public PlayerFactory(NetworkPrefabFactory networkPrefabFactory, ITurnService turnService)
        {
            _networkPrefabFactory = networkPrefabFactory;
            _turnService = turnService;
        }

        public T CreatePlayer<T>(string prefabName) where T : Player
        {
            var player = _networkPrefabFactory.Create(prefabName).GetComponent<T>();
            if (Application.platform == RuntimePlatform.Android)
            {
                player.gameObject.AddComponent<TouchInput>().Init(player, _turnService);
            }
            else
            {
                player.gameObject.AddComponent<MouseInput>().Init(player, _turnService);
            }
            return player;
        }
    }
}