using System.Collections.Generic;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Variables;
using UnityEngine;

namespace PV3.Character
{
    public enum CombatClass { Warrior, Wizard, Ranger }
    public abstract class CharacterObject : ScriptableObject
    {
        [System.Serializable]
        public class Spells
        {
            public SpellObject spell;
            public bool isOnCooldown;
            public int cooldownTimer;
        }

        [Header("Base Information")]
        public new string name = string.Empty;
        public Sprite portraitSprite;
        public IntValue Level;
        public CombatClass Class;

        [Header("Stats")]
        public AttributesObject Attributes;
        public IntValue MaxHealth;
        public IntValue CurrentHealth;

        [Header("Status Effects")]
        public CurrentStatusEffectObject StatusEffectObject;

        [Header("")]
        public List<Spells> ListOfSpells;

        //Sub-attributes of every character.
        private float BlockChance { get; set; }
        private float DodgeChance { get; set; }
        private float DamageReduction { get; set; }
        private float CriticalChance { get; set; }

        public void InitializeHealthValues()
        {
            MaxHealth.Value = 5 * Attributes.Constitution + 2 * Level.Value;
            CurrentHealth.Value = MaxHealth.Value;
        }

        public void InitializeSubAttributes()
        {
            DodgeChance = 0;
            BlockChance = 0;
            DamageReduction = Mathf.Abs(0.5f * Attributes.Armor);
            CriticalChance = Mathf.Abs(0.5f * Attributes.Dexterity);
        }

        public bool HasBlockedAttack()
        {
            // Block Chance is formatted to [#.#%], so the RNG value must be [#.#%] as well.
            return BlockChance + StatusEffectObject.BonusObject.BlockBonus >= Mathf.Round(Random.Range(0f, 100f) * 10f) / 10f;
        }

        public bool HasDodgedAttack()
        {
            // Dodge Chance is formatted to [#.#%], so the RNG value must be [#.#%] as well.
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
            if (type == AttributeType.Strength) return Attributes.Strength;
            if (type == AttributeType.Dexterity) return Attributes.Dexterity;
            if (type == AttributeType.Constitution) return Attributes.Constitution;
            if (type == AttributeType.Intelligence) return Attributes.Intelligence;
            if (type == AttributeType.Armor) return Attributes.Armor;

            return 0;
        }
        public void DeductHealth(int value)
        {
            CurrentHealth.Value -= value;

            if (CurrentHealth.Value < 0)
            {
                CurrentHealth.Value = 0;
            }
        }

        public void AddHealth(int value)
        {
            CurrentHealth.Value += value;

            if (CurrentHealth.Value > MaxHealth.Value)
            {
                CurrentHealth.Value = MaxHealth.Value;
            }
        }
    }
}