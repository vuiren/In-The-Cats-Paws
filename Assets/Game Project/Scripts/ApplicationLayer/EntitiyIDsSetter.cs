using System.Collections.Generic;
using System.Linq;
using Game_Project.Scripts.CommonLayer;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Interfaces;
using Game_Project.Scripts.DataLayer.Level;
using Game_Project.Scripts.ViewLayer.Entities.Base;
using UnityEngine;

namespace Game_Project.Scripts.ApplicationLayer
{
    public class EntitiyIDsSetter
    {
        public void SetIDs()
        {
            SetEntitiesIds<Room>();
            SetEntitiesIds<Corridor>();
         //   SetEntitiesIds<Button>();
        }

        private void SetEntitiesIds<T>() where T: EntityModel
        {
            SetIds(GetEntities<T>());
        }

        private void SetIds<T>(IReadOnlyList<Entity<T>> entities) where T : IEntityWithID, IEntityWithGameObjectLink
        {
            var logger = new MyLogger(this);
            logger.Log($"Start setting ids for {typeof(T)} {entities.Count} entities");

            for (var index = 0; index < entities.Count; index++)
            {
                var entity = entities[index];
                logger.Log($"Setting id: {index} for entity {entity.GameObjectLink.name}");

                entity.model.ID = index;
            }
            logger.Log($"Done setting ids");
        }

        public static Entity<T>[] GetEntities<T>() where T : IEntityWithID, IEntityWithGameObjectLink
        {
            var entities = Object.FindObjectsOfType<Entity<T>>()
                .ToArray();
            
            return entities;
        }
    }
}