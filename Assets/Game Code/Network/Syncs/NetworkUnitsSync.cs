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
            _logger.Log(this,$"Registering unit {unitName} over network");
            var unitGo = GameObject.Find(unitName);
            if (!unitGo)
            {
                _logger.LogError($"{unitName} has not been found on the scene");
                return;
            }
			
            var unit = unitGo.GetComponent<Unit>();
            if (!unit)
            {
                _logger.LogError($"{unit.gameObject.name} is not unit");
                return;
            }
			
            _unitsService.RegisterUnit(unit, unit.unitType);
            _logger.Log(this,$"Done registering unit {unitName} over network");
        }
        
        [PunRPC]
        private void RefreshUnitsModelRPC()
        {
            _logger.Log(this,"Registering units on scene");

            var units = FindObjectsOfType<Unit>();
            _logger.Log(this,$"{units.Length} units has been found");
            foreach (var unit in units)
            {
                _unitsService.RegisterUnit(unit, unit.unitType);
            }
            _logger.Log(this,"Done registering units on scene");
        }
    }
}