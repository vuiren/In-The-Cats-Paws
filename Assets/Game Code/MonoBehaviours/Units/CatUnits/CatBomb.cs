using System;
using Game_Code.Controllers.CatBotControllers;
using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Units.CatUnits.UI;
using Game_Code.Network.Syncs;
using Game_Code.Services;
using UnityEngine;
using Zenject;

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
        void SetState(CatBombState state);
        void StartExploding(int turnsCount);
    }

    public class CatBomb: Unit, ICatBomb
    {
        [SerializeField] private CatBombState catBombState = CatBombState.NotExploding;
        [SerializeField] private ExplosionUI explosionUI;
        [SerializeField] private int revivingTurnsCount = 1;
        
        private INetworkCatBombExplosionSync _explosionSync;
        public int turnsUntilRevival;
        public CatBombState GetCatBombState() => catBombState;
        public void SetState(CatBombState state)
        {
            catBombState = state;
        }

        [Inject]
        public void ConstructCatBomb(ILogger logger, StaticData staticData, IRoomsService roomsService, 
            IUnitsService unitsService, ITurnService turnService, INetworkTurnsSync networkTurnsSync, 
            INetworkCatBombExplosionSync explosionSync, ICatBotExplosionController explosionController)
        {
            base.Construct(logger, staticData, roomsService, unitsService, turnService, networkTurnsSync);
            explosionUI.Construct(turnService,this, explosionController);
            _explosionSync = explosionSync;
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
                    Logger.Log(this,$"{gameObject.name} is doing nothing");
                    break;
                case CatBombState.Reviving:
                    turnsUntilRevival--;
                    Logger.Log(this,$"{turnsUntilRevival} turns left until {gameObject.name} revival");
                    if (turnsUntilRevival <= 0)
                    {
                        Revive();
                    }
                    break;
                case CatBombState.Exploding:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Revive()
        {
            Logger.Log(this,$"{gameObject.name} is available again");

            catBombState = CatBombState.NotExploding;
        }

        public void StartExploding(int turnsCount)
        {
            _explosionSync.StartExplosion(this, turnsCount);
            TurnsSync.EndCurrentTurn();
        }

        public void ShowUI()
        {
            explosionUI.gameObject.SetActive(true);
        }

        public void HideUI()
        {
            explosionUI.gameObject.SetActive(false);
        }
    }
}