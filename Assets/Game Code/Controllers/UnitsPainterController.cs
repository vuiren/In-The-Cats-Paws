using Game_Code.MonoBehaviours.Data;
using Game_Code.MonoBehaviours.Units;
using Game_Code.Services;
using UnityEngine;

namespace Game_Code.Controllers
{
    public class UnitsPainterController
    {
        private readonly ILogger _logger;
        private readonly IUnitsSelectionService _selectionService;
        private StaticData _staticData;
        
        public UnitsPainterController(ILogger logger, StaticData staticData, IUnitsSelectionService selectionService)
        {
            _logger = logger;
            _selectionService = selectionService;
            _staticData = staticData;
            
            _selectionService.RegisterOnUnitSelection(PaintUnitWithSelectionColor);
            _selectionService.RegisterOnUnitDeselection(PaintUnitWithNormalColor);
        }

        private void PaintUnitWithNormalColor(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log($"Painting unit {unitGo.name} with normal color");
            var renderer = unitGo.GetComponentInChildren<SpriteRenderer>();
            renderer.color = _staticData.unitNormalColor;
            _logger.Log($"Done painting unit {unitGo.name}");
        }

        private void PaintUnitWithSelectionColor(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log($"Painting unit {unitGo.name} with selection color");
            var renderer = unitGo.GetComponentInChildren<SpriteRenderer>();
            renderer.color = _staticData.unitSelectedColor;
            _logger.Log($"Done painting unit {unitGo.name}");
        }
    }
}