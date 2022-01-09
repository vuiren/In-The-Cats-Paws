using DG.Tweening;
using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer.Controllers.Drawers
{
    public sealed class SelectedUnitDrawer
    {
        public SelectedUnitDrawer(IUnitsSelectionService selectionService)
        {
            selectionService.RegisterOnUnitSelection(ScaleUnit);
            selectionService.RegisterOnUnitDeselection(DescaleUnit);
        }

        private void DescaleUnit(Unit obj)
        {
            obj.GameObjectLink.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }

        private void ScaleUnit(Unit obj)
        {
            obj.GameObjectLink.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.5f);
        }
    }
}