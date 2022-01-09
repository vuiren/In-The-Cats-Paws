using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Syncs
{
    public interface INetworkUnitsPositionSync
    {
        void SetUnitPosition(int unitId, Vector3 position);
    }
    
    [RequireComponent(typeof(PhotonView))]
    public sealed class NetworkUnitsPositionSync: MonoBehaviour, INetworkUnitsPositionSync
    {
        private IMyLogger _myLogger;
        private IUnitPositionService _unitPositionService;
        private PhotonView _photonView;
        
        [Inject]
        public void Construct(IMyLogger myLogger, IUnitPositionService unitPositionService)
        {
            _myLogger = myLogger;
            _unitPositionService = unitPositionService;
            _photonView = GetComponent<PhotonView>();
        }

        [PunRPC]
        private void SetUnitPositionRPC(int unitID, Vector3 position)
        {
            /*_myLogger.Log( $"Setting unit with id {unitID} position over network");
            var unit = _unitViewService.GetUnitViewById(unitID);
            if (!unit) return;
            
            _unitPositionService.SetUnitPosition(unit, position);
            _myLogger.Log( $"Unit {unit.GetUnitModel().UnitType} position has been set over network");*/
        }

        public void SetUnitPosition(int unitId, Vector3 position)
        {
            _photonView.RPC("SetUnitPositionRPC", RpcTarget.All, unitId, position);
        }
    }
}