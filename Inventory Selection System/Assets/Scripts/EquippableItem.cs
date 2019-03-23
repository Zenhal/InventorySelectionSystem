using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterStats;
using UnityEditor;
using System;

public enum EquipmentType
{
    Weapon,
    Head,
    Body,
    Feet,
}
public enum EquipmentClass
{
    Common,
    Uncommon,
    Rare,
    Legendary,
    Mythical,
}

[CreateAssetMenu]
public class EquippableItem : Item
{
    public EquipmentClass EquipmentClass;
    [Space]
    public int Strength;
    public int Agility;
    public int Intelligence;
    public int Damage;
    public int Defence;
    [Space]
    public int HP;
    public int Mana;
    public double DodgeChance;
    public double CritChance;
    public EquipmentType EquipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }



    public void Equip( Character c)
    {
        if (Strength != 0)
        {
            c.Strength.AddModifier(new StatModifier(Strength, StatModType.Flat, this));
            c.HP.AddModifier(new StatModifier(Strength * 12, StatModType.Flat, this));
        }
        if (Agility != 0)
        {
            c.Agility.AddModifier(new StatModifier(Agility, StatModType.Flat, this));
            c.DodgeChance.AddModifier(new StatModifier(Agility * 0.2, StatModType.Flat, this));
            c.CritChance.AddModifier(new StatModifier(Agility * 0.15, StatModType.Flat, this));
        }
        if (Intelligence != 0)
        {
            c.Intelligence.AddModifier(new StatModifier(Intelligence, StatModType.Flat, this));
            c.Mana.AddModifier(new StatModifier(Intelligence * 14, StatModType.Flat, this));
        }
        if (Defence != 0)
            c.Defence.AddModifier(new StatModifier(Defence, StatModType.Flat, this));
        if (Damage != 0)
            c.Damage.AddModifier(new StatModifier(Damage, StatModType.Flat, this));

    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
        c.Defence.RemoveAllModifiersFromSource(this);
        c.Damage.RemoveAllModifiersFromSource(this);
        c.HP.RemoveAllModifiersFromSource(this);
        c.Mana.RemoveAllModifiersFromSource(this);
        c.DodgeChance.RemoveAllModifiersFromSource(this);
        c.CritChance.RemoveAllModifiersFromSource(this);

    }
}
