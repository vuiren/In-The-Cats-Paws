using UnityEngine;

namespace Game_Code.MonoBehaviours.Units
{
	[RequireComponent(typeof(Unit))]
	public class UnitPainter : MonoBehaviour
	{
		[SerializeField] SpriteRenderer spriteRenderer;
		[SerializeField] Color choosedColor, unChoosedColor;
		Unit _unit;

		private void Awake()
		{
			_unit = GetComponent<Unit>();
		}

		private void OnEnable()
		{
			_unit = GetComponent<Unit>();

			_unit.OnUnitChose += PaintUnit;
			_unit.OnUnitUnchoose += UnPaintUnit;
		}

		private void UnPaintUnit()
		{
			spriteRenderer.color = unChoosedColor;
		}

		private void PaintUnit()
		{
			spriteRenderer.color = choosedColor;
		}

		private void OnDisable()
		{
			_unit.OnUnitChose -= PaintUnit;
			_unit.OnUnitUnchoose -= UnPaintUnit;
		}
	}
}
