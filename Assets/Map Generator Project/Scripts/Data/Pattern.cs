using System;
using UnityEngine;

namespace Map_Generator_Project.Scripts
{
    public enum UnitType
    {
        Engineer,
        CatBomb,
        CatBite,
        CatButton,
    }
    
    [Serializable]
    public class DirectionToSprite
    {
        public Direction[] directions;
        public Sprite sprite;    
    }

    [Serializable]
    public sealed class UnitTypeToSprite
    {
        public UnitType unitType;
        public Sprite sprite;
    }
    
    [CreateAssetMenu(fileName = "New Pattern", menuName = "Add Pattern")]
    public class Pattern : ScriptableObject
    {
        public DirectionToSprite[] directionToSprites;
        public UnitTypeToSprite[] unitTypeToSprites;
    }
}