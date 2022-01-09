using Game_Project.Scripts.ViewLayer.Entities.Level;
using Game_Project.Scripts.ViewLayer.Entities.Units;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Factories
{
    public interface IUnitFactory
    {
        UnitView CreateUnit(GameObject unitPrefab, SpawnPointView spawnPoint);
    }
}