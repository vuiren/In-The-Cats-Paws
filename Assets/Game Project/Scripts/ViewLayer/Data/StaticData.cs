using System;
using Game_Project.Scripts.DataLayer;
using Game_Project.Scripts.DataLayer.Units;
using UnityEngine;

namespace Game_Project.Scripts.ViewLayer.Data
{
	[Serializable]
	public sealed class UnitTypeToPrefab
	{
		public UnitType unitType;
		public GameObject prefab;
	}

	[Serializable]
	public sealed class Effect
	{
		public EffectType effectType;
		public float effectLiveTime;
		public GameObject effectPrefab;
	}

	public enum SoundType
	{
		EngineerGameMusic,
		SmartCatGameMusic,
	}
	
	[Serializable]
	public sealed class Sound
	{
		
	}

	[CreateAssetMenu(fileName = "Config", menuName = "Config/Add Config")]
	public sealed class StaticData : ScriptableObject
	{
		public LayerMask catLayerMask, engineerLayerMask;
		public AudioClip engineerGameMusic, catGameMusic;
		public UnitTypeToPrefab[] unitTypeToPrefabs;
		public Effect[] effects;
		public int playersCount = 2;
		public float unitMoveSpeed;
	}
}