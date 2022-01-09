using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Interfaces
{
    public interface IEntityWithGameObjectLink
    {
        GameObject GameObjectLink { get; set; }
    }
}