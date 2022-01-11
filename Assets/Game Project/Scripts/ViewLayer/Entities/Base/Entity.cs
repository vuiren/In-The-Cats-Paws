using Game_Project.Scripts.DataLayer.Interfaces;
using UnityEngine;
using Zenject;

namespace Game_Project.Scripts.ViewLayer.Entities.Base
{
    public abstract class Entity<T>: MonoBehaviour, IEntityWithID, IEntityWithGameObjectLink 
        where T: IEntityWithID, IEntityWithGameObjectLink
    {
        public T model;

        protected abstract void SetModel();
        public int ID
        {
            get => model.ID;
            set => model.ID = value;
        }
        public GameObject GameObjectLink
        {
            get => model.GameObjectLink;
            set => model.GameObjectLink = value;
        }

        [Inject]
        public virtual void Construct()
        { 
            SetModel();
            model.GameObjectLink = gameObject;
        }
    }
}