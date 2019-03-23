using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CharacterStats
{
	[Serializable]
	public class CharacterStat
	{
		public double BaseValue;

		protected bool isDirty = true;
		protected double lastBaseValue;

		protected double _value;
		public virtual double Value {
			get {
				if(isDirty || lastBaseValue != BaseValue) {
					lastBaseValue = BaseValue;
					_value = CalculateFinalValue();
					isDirty = false;
				}
				return _value;
			}
		}

		protected readonly List<StatModifier> statModifiers;
		public readonly ReadOnlyCollection<StatModifier> StatModifiers;

		public CharacterStat()
		{
			statModifiers = new List<StatModifier>();
			StatModifiers = statModifiers.AsReadOnly();
		}

		public CharacterStat(float baseValue) : this()
		{
			BaseValue = baseValue;
		}

		public virtual void AddModifier(StatModifier mod)
		{
			isDirty = true;
			statModifiers.Add(mod);
		}

		public virtual bool RemoveModifier(StatModifier mod)
		{
			if (statModifiers.Remove(mod))
			{
				isDirty = true;
				return true;
			}
			return false;
		}

		public virtual bool RemoveAllModifiersFromSource(object source)
		{
			int numRemovals = statModifiers.RemoveAll(mod => mod.Source == source);

			if (numRemovals > 0)
			{
				isDirty = true;
				return true;
			}
			return false;
		}

		protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
		{
			if (a.Order < b.Order)
				return -1;
			else if (a.Order > b.Order)
				return 1;
			return 0; 
		}
		
		protected virtual double CalculateFinalValue()
		{
			double finalValue = BaseValue;

			statModifiers.Sort(CompareModifierOrder);

			for (int i = 0; i < statModifiers.Count; i++)
			{
				StatModifier mod = statModifiers[i];

				if (mod.Type == StatModType.Flat)
				{
					finalValue += mod.Value;
				}
			}

			
			return (double)Math.Round(finalValue, 4);
		}
	}
}
