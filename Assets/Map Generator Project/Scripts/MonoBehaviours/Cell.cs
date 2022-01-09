using System;
using System.Collections.Generic;
using System.Linq;
using Map_Generator_Project.Scripts.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map_Generator_Project.Scripts.MonoBehaviours
{
    public class Cell: MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] public CellModel cellModel;
        [SerializeField] public SpriteRenderer spriteRenderer;
        [SerializeField] private Pattern pattern;

        public int index;
        
        public Action<Cell,PointerEventData> OnCellClicked { get; set; }
        
        public void Construct(Direction[] directions, Vector2Int coords)
        {
            cellModel.x = coords.x;
            cellModel.y = coords.y;
            cellModel.directions = directions;
            
            var sprite = pattern.directionToSprites
                .SingleOrDefault(x => CompareTwoArrays(x.directions, directions))
                ?.sprite;

            spriteRenderer.sprite = sprite == null ? spriteRenderer.sprite : sprite;
        }

        private bool CompareTwoArrays(Direction[] directions1, Direction[] directions2)
        {
            var isEqual = new HashSet<Direction>(directions1).SetEquals(directions2);

            return isEqual;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"Clicked cell {gameObject.name}");
            OnCellClicked?.Invoke(this, eventData);
           
        }
    }
}