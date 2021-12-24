using Game_Code.Controllers.CatBotControllers;
using Game_Code.MonoBehaviours.Units.CatUnits;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkCatBombExplosionSync
    {
        void StartExplosion(CatBomb catBomb, int turnsCount);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkCatBombExplosionSync: MonoBehaviour, INetworkCatBombExplosionSync
    {
        private ICatBotExplosionController _explosionController;
        private PhotonView _photonView;
        private IUnitsService _unitsService;
        private ILogger _logger;
        
        [Inject]
        public void Construct(ILogger logger, IUnitsService unitsService, 
            ICatBotExplosionController explosionController)
        {
            _logger = logger;
            _explosionController = explosionController;
            _unitsService = unitsService;
            _photonView = GetComponent<PhotonView>();
        }


        public void StartExplosion(CatBomb catBomb, int turnsCount)
        {
            _photonView.RPC("StartExplosionRPC", RpcTarget.All, catBomb.gameObject.name, turnsCount);
        }

        [PunRPC]
        private void StartExplosionRPC(string catBombName, int turnsCount)
        {
            _logger.Log(this, "Starting to explode over the network");
            var catBomb = _unitsService.GetUnitByName(catBombName) as CatBomb;
            if(catBomb == null) return;
            
            _explosionController.StartExploding(catBomb, turnsCount);
        }
    }
}