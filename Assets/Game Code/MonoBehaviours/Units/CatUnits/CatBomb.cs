using System;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.UI;
using Game_Code.MonoBehaviours.Units.CatUnits.UI;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Units.CatUnits
{
    public enum CatBombState
    {
        NotExploding,
        Exploding,
        Reviving
    }

    public interface ICatBomb
    {
        CatBombState GetCatBombState();
        void StartExploding(int turnsCount);
        int TurnLeftUntilExplosion();
    }
    
    public class CatBomb: Unit, ICatBomb
    {
        [SerializeField] private CatBombState catBombState = CatBombState.NotExploding;
        [SerializeField] private ExplosionUI explosionUI;
        [SerializeField] private int revivingTurnsCount = 1;
        
        private int _turnsUntilExplosion;
        public int turnsUntilRevival;
        public CatBombState GetCatBombState() => catBombState;

        public override void Construct(ILogger logger, StaticData staticData, IRoomsService roomsService, IUnitsService unitsService,
            ITurnService turnService, INetworkTurnsSync networkTurnsSync)
        {
            base.Construct(logger, staticData, roomsService, unitsService, turnService, networkTurnsSync);
            explosionUI.Construct(turnService,this);
            TurnService.OnEngineerTurn(TurnTick);
            TurnService.OnSmartCatTurn(TurnTick);
        }

        private void Start()
        {
            explosionUI.gameObject.SetActive(false);
        }
        
        private void TurnTick()
        {
            switch (catBombState)
            {
                case CatBombState.NotExploding:
                    Logger.Log($"{gameObject.name} is doing nothing");
                    break;
                case CatBombState.Exploding:
                    _turnsUntilExplosion--;
                    Logger.Log($"{_turnsUntilExplosion} turns left until {gameObject.name} explode!");

                    if (_turnsUntilExplosion <= 0)
                    {
                        Explode();
                    }
                    break;
                case CatBombState.Reviving:
                    turnsUntilRevival--;
                    Logger.Log($"{turnsUntilRevival} turns left until {gameObject.name} revival");
                    if (turnsUntilRevival <= 0)
                    {
                        Revive();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Revive()
        {
            Logger.Log($"{gameObject.name} is available again");

            catBombState = CatBombState.NotExploding;
        }

        private void Explode()
        {
            Logger.Log($"{gameObject.name} is exploding!");
            catBombState = CatBombState.Reviving;
            turnsUntilRevival = revivingTurnsCount;
        }

        public void StartExploding(int turnsCount)
        {
            Logger.Log($"{gameObject.name} will explode in {turnsCount} turns!");

            _turnsUntilExplosion = turnsCount;
            catBombState = CatBombState.Exploding;
            TurnsSync.EndCurrentTurn();
        }

        public int TurnLeftUntilExplosion() => _turnsUntilExplosion;

        /*public override void SelectUnit()
        {
            base.SelectUnit();
            explosionUI.gameObject.SetActive(true);
        }

        public override void DeselectUnit()
        {
            base.DeselectUnit();
            explosionUI.gameObject.SetActive(false);
        }*/
    }
}