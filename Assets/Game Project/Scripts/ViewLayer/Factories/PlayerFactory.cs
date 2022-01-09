using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;

namespace Game_Project.Scripts.ViewLayer.Factories
{
    public class PlayerFactory
    {
        private readonly ITurnService _turnService;

        public PlayerFactory(ITurnService turnService)
        {
            _turnService = turnService;
        }

        public T CreatePlayer<T>(string prefabName) where T : Player
        {
            return null;
            /*var player = _networkPrefabFactory.Create(prefabName).GetComponent<T>();
            if (Application.platform == RuntimePlatform.Android)
            {
                player.GameObjectLink().AddComponent<TouchInput>().Init(player.GameObjectLink().GetComponent<PlayerView>(), 
                    _turnService);
            }
            else
            {
                player.GameObjectLink().AddComponent<MouseInput>().Init(player.GameObjectLink().GetComponent<PlayerView>(), _turnService);
            }
            return player;*/
        }
    }
}