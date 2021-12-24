using DG.Tweening;
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
            
            _selectionService.RegisterOnUnitSelection(ScaleUnit);
            _selectionService.RegisterOnUnitDeselection(DescaleUnit);
        }

        private void ScaleUnit(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log(this, $"Scaling unit {unitGo.name}");
            unitGo.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 1);
            _logger.Log(this, $"Done scaling unit {unitGo.name}");        
        }
        
        private void DescaleUnit(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log(this, $"Scaling unit {unitGo.name}");
            unitGo.transform.DOScale(Vector3.one, 1);
            _logger.Log(this, $"Done scaling unit {unitGo.name}");        
        } 

        private void PaintUnitWithNormalColor(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log(this, $"Painting unit {unitGo.name} with normal color");
            var renderer = unitGo.GetComponentInChildren<SpriteRenderer>();
            renderer.color = _staticData.unitNormalColor;
            _logger.Log(this, $"Done painting unit {unitGo.name}");
        }

        private void PaintUnitWithSelectionColor(IUnit unit)
        {
            var unitGo = unit.UnitGameObject();
            _logger.Log(this, $"Painting unit {unitGo.name} with selection color");
            var renderer = unitGo.GetComponentInChildren<SpriteRenderer>();
            renderer.color = _staticData.unitSelectedColor;
            _logger.Log(this, $"Done painting unit {unitGo.name}");
        }
    }
}