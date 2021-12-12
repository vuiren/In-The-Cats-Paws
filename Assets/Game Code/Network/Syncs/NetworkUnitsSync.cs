using Game_Code.MonoBehaviours.Units;
using Game_Code.Services;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Code.Network.Syncs
{
    public interface INetworkUnitsSync
    {
        void RefreshUnitsModel();
        void RegisterUnit(string unitName);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public class NetworkUnitsSync: MonoBehaviour, INetworkUnitsSync
    {
        private PhotonView _photonView;
        private ILogger _logger;
        private IUnitsService _unitsService;

        [Inject]
        public void Construct(ILogger logger, IUnitsService unitsService)
        {
            _logger = logger;
            _unitsService = unitsService;
            _photonView = GetComponent<PhotonView>();
        }
        
        public void RefreshUnitsModel()
        {
            _photonView.RPC("RefreshUnitsModelRPC", RpcTarget.All);
        }

        public void RegisterUnit(string unitName)
        {
            _photonView.RPC("RegisterUnitRPC", RpcTarget.All, unitName);
        }
        
        [PunRPC]
        private void RegisterUnitRPC(string unitName)
        {
            _logger.Log($"Registering unit {unitName} over network");
            var unitGO = GameObject.Find(unitName);
            if (!unitGO)
            {
                _logger.LogError($"{unitName} has not been found on the scene");
                return;
            }
			
            var unit = unitGO.GetComponent<Unit>();
            if (!unit)
            {
                _logger.LogError($"{unit.gameObject.name} is not unit");
                return;
            }
			
            _unitsService.RegisterUnit(unit, unit.unitType);
        }
        
        [PunRPC]
        private void RefreshUnitsModelRPC()
        {
            _logger.Log("Refreshing units model");

            var units = FindObjectsOfType<Unit>();
            _logger.Log($"{units.Length} units has been found");
            foreach (var unit in units)
            {
                _unitsService.RegisterUnit(unit, unit.unitType);
            }
        }
    }
}