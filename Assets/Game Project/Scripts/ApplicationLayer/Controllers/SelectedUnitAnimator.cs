using Game_Project.Scripts.DataLayer.Units;
using Game_Project.Scripts.LogicLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ApplicationLayer.Controllers
{
    public sealed class SelectedUnitAnimator
    {
        private readonly IUnitsSelectionService _selectionService;

        [Inject]
        public SelectedUnitAnimator(IUnitsSelectionService selectionService)
        {
            _selectionService = selectionService;
            _selectionService.RegisterOnUnitSelection(AnimateUnit);
        }

        private void AnimateUnit(Unit unit)
        {
            var animator = unit.GameObjectLink.GetComponentInChildren<Animator>();
            if (animator)
                animator.Play("Selected");
        }
    }
}