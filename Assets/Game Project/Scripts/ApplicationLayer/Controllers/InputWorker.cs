using Game_Project.Scripts.ApplicationLayer.Controllers.UnitControllers;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.CommonLayer.Factories;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.LogicLayer.Interfaces;
using Game_Project.Scripts.ViewLayer.Data;
using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using Mono.CSharp;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class InputWorker
    {
        private IInputService _inputService;
        private readonly LayerMask layerMask;
        private readonly UnitMovingController _unitMovingController;
        private readonly IUnitsSelectionService _unitsSelectionService;
        private readonly ITurnService _turnService;
        private readonly PlayerType _playerType;
        private IMyLogger _logger;

        [Inject]
        public InputWorker(IInputService inputService, UnitMovingController unitMovingController,
            IUnitsSelectionService unitsSelectionService, StaticData staticData,
            ICurrentPlayerService currentPlayerService,
            ITurnService turnService)
        {
            _logger = LoggerFactory.Create(this);
            _playerType = currentPlayerService.CurrentPlayerType();
            layerMask = _playerType == PlayerType.Engineer
                ? staticData.engineerLayerMask
                : staticData.catLayerMask;

            _turnService = turnService;
            _unitMovingController = unitMovingController;
            _unitsSelectionService = unitsSelectionService;
            inputService.OnPlayerClick(MakeTurn);
        }

        public void MakeTurn(Vector3 pointerWorldPosition)
        {
            if (_playerType.ToString() != _turnService.CurrentTurn().ToString()) return;

            _logger.Log("Click");
            var hit = Physics2D.Raycast(pointerWorldPosition, -Vector2.up, 100, layerMask);

            if (!hit) return;
            
            var unitView = hit.transform.parent.GetComponent<UnitView>();
            if (unitView)
            {
                _unitsSelectionService.SelectUnit(unitView.ID);
                return;
            }

            var roomView = hit.transform.parent.GetComponent<RoomView>();
            if (roomView)
            {
                var unit = _unitsSelectionService.GetSelectedUnit();
                if (unit != null)
                {
                    if (_unitMovingController.TryToSendUnitToRoom(unit.ID, roomView.model.Coords))
                        _turnService.EndCurrentTurn();
                }
            }


        }
    }
}