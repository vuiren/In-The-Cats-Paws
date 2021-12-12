using Game_Code.MonoBehaviours.Players;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkPlayersSync
    {
        void RegisterPlayer(Player player);
        void RefreshPlayers();
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkPlayersSync: MonoBehaviour, INetworkPlayersSync
    {
        private PhotonView _photonView;
        private ILogger _logger;
        private IPlayersService _playersService;
        private INetworkTurnsSync _turnsSync;

        [Inject]
        public void Construct(ILogger logger, IPlayersService playersService, INetworkTurnsSync networkTurnsSync)
        {
            _logger = logger;
            _playersService = playersService;
            _turnsSync = networkTurnsSync;
            _photonView = GetComponent<PhotonView>();
        }

        public void RefreshPlayers()
        {
            _photonView.RPC("SearchForPlayersRPC", RpcTarget.All);
        }
        
        [PunRPC]
        private void SearchForPlayersRPC()
        {
            var playerEngineer = FindObjectOfType<PlayerEngineer>();
            var playerSmartCat = FindObjectOfType<PlayerSmartCat>();

            if (playerEngineer != null)
            {
                _playersService.RegisterPlayer(playerEngineer);
            }

            if (playerSmartCat != null)
            {
                _playersService.RegisterPlayer(playerSmartCat);
            }

            if (playerEngineer)
            {
                _logger.Log("Player Engineer has been set");
            }
            else
            {
                _logger.LogWarning("Player Engineer has not been found");
            }

            if (!playerEngineer || !playerSmartCat) return;
            _logger.Log($"All Players has been set");

            var transformParent = transform;
            playerEngineer.transform.parent = transformParent;
            playerSmartCat.transform.parent = transformParent;

            playerEngineer.OnStepMade += _turnsSync.EndCurrentTurn;
            playerSmartCat.OnStepMade += _turnsSync.EndCurrentTurn;
        }
        
        [PunRPC]
        private void RegisterPlayerRPC(string playerName)
        {
            _logger.Log($"Registering player {playerName} over network");
            var playerGO = GameObject.Find(playerName);
            if (!playerGO)
            {
                _logger.LogError($"{playerName} has not been found on the scene");
                return;
            }
			
            var player = playerGO.GetComponent<Player>();
            if (!player)
            {
                _logger.LogError($"{playerGO.name} is not a player");
                return;
            }
			
            _playersService.RegisterPlayer(player);
        }

        public void RegisterPlayer(Player player)
        {
            _photonView.RPC("RegisterPlayerRPC", RpcTarget.All, player.gameObject.name);
        }
    }
}