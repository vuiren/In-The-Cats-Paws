using System;
using System.Collections.Generic;
using Game_Code.MonoBehaviours.Units;
using UnityEngine;

namespace Game_Code.MonoBehaviours.Data
{
	[Serializable]
	public class UnitPrefab
	{
		public UnitType unitType;
		public GameObject prefab;
	}

	[CreateAssetMenu(fileName = "Config", menuName = "Config/Add Config")]
	public class StaticData : ScriptableObject
	{
		public GameObject playerEngineerPrefab;
		public GameObject playerSmartCatPrefab;
		public UnitPrefab engineerCharacterPrefab;
		public List<UnitPrefab> smartCatBots;
		public Color unitSelectedColor = Color.red, unitNormalColor = Color.white;
		public int playersCount = 2;
		public float unitMoveSpeed;
	}
}