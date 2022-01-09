using System;
using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.NetworkLayer.Base;
using Photon.Pun;
using Zenject;

namespace Game_Project.Scripts.NetworkLayer.Services
{
    public sealed class NetworkUnitExplosionService: NetworkService, IUnitExplosionService
    {
        private IMyLogger _logger;
        private IUnitsService _unitsService;
        private Action<Unit> _onUnitExplosion, _onUnitExplosionStart;
        private readonly Dictionary<int, int> _unitTurnsUntilExplosion = new(); //key - unitId, value - turnsLeft

        [Inject]
        public void CustomConstruct(IUnitsService unitsService)
        {
            base.Construct();
            _logger = LoggerFactory.Create(this);
            _unitsService = unitsService;
        }

        public Tuple<int, int>[] GetAll()
        {
            return _unitTurnsUntilExplosion.Select(x => new Tuple<int, int>(x.Key, x.Value)).ToArray();
        }

        public void OnUnitExplosionStart(Action<Unit> action)
        {
            _onUnitExplosionStart += action;
        }

        public int TurnLeftUntilUnitExplode(int unitId)
        {
            if (_unitTurnsUntilExplosion.ContainsKey(unitId))
            {
                return _unitTurnsUntilExplosion[unitId];
            }
            
            _logger.LogWarning($"Unit with id {unitId} is not registered for explosion");
            return -1;
        }

        public void RegisterUnitForExplosion(Unit unit, int turnsUntilExplosion)
        {
            PhotonView.RPC("RegisterUnitForExplosionRPC", RpcTarget.All,
                unit.ID, turnsUntilExplosion);
        }
        
        [PunRPC]
        private void RegisterUnitForExplosionRPC(int unitId, int turnsUntilExplosion)
        {
            var unit = _unitsService.GetUnitByID(unitId);
            _unitTurnsUntilExplosion.Add(unit.ID, turnsUntilExplosion);
            _onUnitExplosionStart?.Invoke(unit);
        }


        public void TickTimer()
        {
            var keys = _unitTurnsUntilExplosion.Keys.ToArray();
            
            for (var i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                
                _unitTurnsUntilExplosion[key]--;

                if (_unitTurnsUntilExplosion[key] <= 0)
                {
                    _logger.Log($"Unit with id {key} explodes");
                    _unitTurnsUntilExplosion.Remove(key);
                    _onUnitExplosion?.Invoke(_unitsService.GetUnitByID(key));
                }
            }
        }

        public void OnUnitExplosion(Action<Unit> action)
        {
            _onUnitExplosion += action;
        }
    }
}