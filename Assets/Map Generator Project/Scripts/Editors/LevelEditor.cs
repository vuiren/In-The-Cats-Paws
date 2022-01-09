using System;
using System.Collections.Generic;
using Map_Generator_Project.Scripts.Data;
using Map_Generator_Project.Scripts.MonoBehaviours;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Map_Generator_Project.Scripts.Editors
{
    public class LevelEditor : MonoBehaviour
    {
        private Pattern _pattern;
        private EditorModeService _editorModeService;
        private MapDrawer _mapDrawer;
        private MapGenerator _mapGenerator;

        [Inject]
        public void Construct(Pattern pattern, EditorModeService editorModeService,
            MapDrawer mapDrawer, MapGenerator mapGenerator)
        {
            _editorModeService = editorModeService;
            _mapDrawer = mapDrawer;
            _mapDrawer.OnMapDrawed += RegisterMap;
            _pattern = pattern;
            _mapGenerator = mapGenerator;
        }

        private void RegisterMap(Dictionary<CellModel, Cell> obj)
        {
            foreach (var cell in obj.Values)
            {
                cell.OnCellClicked += ChangeCell;
            }
        }

        private void ChangeCell(Cell cell, PointerEventData eventData)
        {
            Debug.Log("Changing cell");
            if (_editorModeService.EditorMode != EditorMode.LevelEdit) return;

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    cell.index--;
                    break;
                case PointerEventData.InputButton.Right:
                    cell.index++;
                    break;
                case PointerEventData.InputButton.Middle:
                    cell.index = -100;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (cell.index == -100)
            {
                cell.cellModel.directions = Array.Empty<Direction>();
                cell.spriteRenderer.sprite = null;
                return;
            }

            if (cell.index < 0)
            {
                cell.index = _pattern.directionToSprites.Length - 1;
            }

            if (cell.index > _pattern.directionToSprites.Length - 1)
            {
                cell.index = 0;
            }

            cell.cellModel.directions = _pattern.directionToSprites[cell.index].directions;
            cell.spriteRenderer.sprite = _pattern.directionToSprites[cell.index].sprite;

            _mapGenerator.UpdateCell(cell.cellModel);
        }
    }
}