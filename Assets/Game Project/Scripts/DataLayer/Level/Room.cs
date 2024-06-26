﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game_Project.Scripts.DataLayer.Level
{
    public sealed class Room : EntityModel
    {
        private Vector2Int coords;
        private bool shadowRoom;
        private List<Vector3> freePoints;

        public List<Vector3> FreePoints
        {
            get => freePoints;
            set => freePoints = value;
        }
        
        public Vector2Int Coords
        {
            get => coords;
            set => coords = value;
        }

        public bool ShadowRoom
        {
            get => shadowRoom;
            set => shadowRoom = value;
        }

        public override string ToString()
        {
            return $"[Room] {base.ToString()} Coords: '{coords}' " +
                   $"Link name: '{(GameObjectLink != null ? GameObjectLink.name : null)}' " +
                   $"Free point left: '{FreePoints.Count}'";
        }
    }
}