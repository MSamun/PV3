// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021 Matthew Samun.
//
// This program is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of  MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
// more details.
//
// You should have received a copy of the GNU General Public License along with
// this program. If not, see <http://www.gnu.org/licenses/>.

using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.ScriptableObjects.Character
{
    public enum CombatClass
    {
        Warrior,
        Wizard,
        Ranger
    }

    public abstract class CharacterObject : ScriptableObject
    {
        [Header("Base Information")]
        public new string name = string.Empty;

        public Sprite portraitSprite;
        public IntValue Level;
        public CombatClass Class;

        [Header("Stats")]
        public AttributesObject Attributes;

        public IntValue MaxHealth;
        public IntValue CurrentHealth;
        public IntValue MaxStamina;
        public IntValue CurrentStamina;

        [Header("")]
        public CurrentStatusEffectObject StatusEffectObject;

        public CharacterSpellsObject SpellsListObject;

        //Sub-attributes of every character.
        public float BlockChance { get; private set; }
        public float DodgeChance { get; private set; }
        public float DamageReduction { get; private set; }
        public float CriticalChance { get; private set; }

        public void InitializeStats()
        {
            MaxHealth.Value = 6 * Attributes.Constitution + 3 * Level.Value;
            CurrentHealth.Value = MaxHealth.Value;

            MaxStamina.Value = 100;
            CurrentStamina.Value = MaxStamina.Value;

            InitializeSubAttributes();
        }

        private void InitializeSubAttributes()
        {
            DodgeChance = 5f;
            BlockChance = 5f;
            DamageReduction = Mathf.Abs(0.5f * Attributes.Armor);
            CriticalChance = Mathf.Abs(0.5f * Attributes.Dexterity);
        }

        public bool HasBlockedAttack()
        {
            // Block Chance is formatted to [#.#]%, so the RNG value must be [#.#]% as well.
            return BlockChance + StatusEffectObject.BonusObject.BlockBonus >= Mathf.RoundToInt(Random.Range(0f, 100f) * 10f) / 10f;
        }

        public bool HasDodgedAttack()
        {
            // Dodge Chance is formatted to [#.#]%, so the RNG value must be [#.#]% as well.
            return DodgeChance + StatusEffectObject.BonusObject.DodgeBonus >= Mathf.RoundToInt(Random.Range(0f, 100f) * 10f) / 10f;
        }

        public bool HasLandedCriticalStrike()
        {
            return CriticalChance + StatusEffectObject.BonusObject.CriticalBonus >= Mathf.RoundToInt(Random.Range(0f, 100f) * 10f) / 10f;
        }

        public float GetDamageReduction()
        {
            return DamageReduction + StatusEffectObject.BonusObject.DamageReductionBonus;
        }

        public float GetDamageBonus()
        {
            return StatusEffectObject.BonusObject.DamageBonus;
        }

        public float GetLifestealBonus()
        {
            return StatusEffectObject.BonusObject.LifestealBonus;
        }

        public int GetAttribute(AttributeType type)
        {
            return type switch
            {
                AttributeType.Strength => Attributes.Strength,
                AttributeType.Dexterity => Attributes.Dexterity,
                AttributeType.Constitution => Attributes.Constitution,
                AttributeType.Intelligence => Attributes.Intelligence,
                AttributeType.Armor => Attributes.Armor,
                AttributeType.None => 0,
                _ => 0
            };
        }

        public void DeductHealth(int value)
        {
            CurrentHealth.Value -= value;

            if (CurrentHealth.Value < 0)
                CurrentHealth.Value = 0;
        }

        public void AddHealth(int value)
        {
            CurrentHealth.Value += value;

            if (CurrentHealth.Value > MaxHealth.Value)
                CurrentHealth.Value = MaxHealth.Value;
        }

        public void DeductStamina(int value)
        {
            CurrentStamina.Value -= value;

            if (CurrentStamina.Value < 0)
                CurrentStamina.Value = 0;
        }

        public void AddStamina(int value)
        {
            CurrentStamina.Value += value;

            if (CurrentStamina.Value > MaxStamina.Value)
                CurrentStamina.Value = MaxStamina.Value;
        }

        public bool IsDead()
        {
            return CurrentHealth.Value <= 0;
        }
    }
}