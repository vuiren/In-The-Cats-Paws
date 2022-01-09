using System;
using Game_Project.Scripts.DataLayer.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer
{
    [Serializable]
    public class EntityModel: IEntityWithID, IEntityWithGameObjectLink
    {
        [SerializeField] [AllowNesting] [ReadOnly] private int id;
        [SerializeField] [AllowNesting] [ReadOnly] private GameObject gameObjectLink;

        public int ID
        {
            get => id;
            set => id = value;
        }

        public GameObject GameObjectLink
        {
            get => gameObjectLink;
            set => gameObjectLink = value;
        }

        public override string ToString()
        {
            return $"Entity ID: {id}, Link: {(gameObjectLink != null ? gameObjectLink.name : null)}";
        }
    }
}