using UnityEngine;

namespace Map_Generator_Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "Map Creation Data", menuName = "Add Map Creation Data")]
    public class MapCreatorStaticData : ScriptableObject
    {
        public GameObject cellPrefab, topDownCorridorPrefab, leftRightCorridorPrefab;
    }
}