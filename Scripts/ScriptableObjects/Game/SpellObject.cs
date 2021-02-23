using System;
using System.Collections.Generic;
using UnityEngine;

namespace PV3.ScriptableObjects.Game
{
    public enum WeaponType { Sword, Staff, Bow }
    public enum SpellType { Damage, Heal, Status }


    [CreateAssetMenu(fileName = "New Spell", menuName = "Spell/New Spell")]
    public class SpellObject : ScriptableObject
    {
        [Header("Base Information")]
        public int spellID;
        public new string name = string.Empty;
        public Sprite sprite;

        [Header("Combat Information")]

        public SpellType spellType;
        [Range(1, 6)] public int totalCooldown;

        [Header("Visuals")]
        public AudioClip SfxClip;
        public Material VfxMaterial;

        [Header("")]
        public List<SpellComponentObject> components;

        public string SetDescription()
        {
            var localDesc = string.Empty;

            for (var i = 0; i < components.Count; i++)
            {
                if (!components[i]) continue;

                if (components[i] is DamageComponent)
                {
                    var tempComponent = components[i] as DamageComponent;
                    var attributeText = DetermineAttributeTypeAndColor(tempComponent.attributeType, tempComponent.attributePercentage);

                    // EASY READ: Deals [# - #] (+##% Attribute) damage. Attacks [#] times.
                    localDesc += $"Deals {tempComponent.minimumValue.ToString()} - {tempComponent.maximumValue.ToString()}" +
                                 $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} damage. " +
                                 $"{(tempComponent.numberOfAttacks > 1 ? $"Attacks {tempComponent.numberOfAttacks.ToString()} times. " : string.Empty)}";
                }
                else if (components[i] is HealComponent)
                {
                    var tempComponent = components[i] as HealComponent;
                    var attributeText = DetermineAttributeTypeAndColor(tempComponent.attributeType, tempComponent.attributePercentage);

                    if (tempComponent.usePercentage)
                    {
                        // EASY READ: Heals for [##%] (+##% Attribute) of your maximum health.
                        localDesc += $"Heals for {tempComponent.maximumValue.ToString()}%" +
                                     $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                     $"of your maximum health. ";
                    }
                    else
                    {
                        // EASY READ: Heals for [# - #] (+##% Attribute) health.
                        localDesc += $"Heals for {tempComponent.minimumValue.ToString()} - {tempComponent.maximumValue.ToString()}" +
                                     $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                     $"health. ";
                    }
                }
                else if (components[i] is StatusComponent)
                {
                    var tempComponent = components[i] as StatusComponent;
                    var attributeText = DetermineAttributeTypeAndColor(tempComponent.attributeType, tempComponent.attributePercentage);

                    if (!tempComponent.isUnique)
                    {
                        // EASY READ: [Increases your/Reduces the target's] (Status Effect) by [##%] (+##% Attribute) for [#] turn(s).
                        localDesc += $"{(tempComponent.isDebuff ? "Reduces the target's" : "Increases your")} " +
                                     $"{(tempComponent.StatusType == StatusType.DamageReduction ? "Damage Reduction" : tempComponent.StatusType.ToString())} by " +
                                     $"{tempComponent.maximumValue.ToString()}%" +
                                     $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                     $"for {tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                    }
                    else
                    {
                        if (tempComponent.StatusType == StatusType.Linger)
                        {
                            if (tempComponent.usePercentage)
                            {
                                // EASY READ: Deals an additional [##%] (+##% Attribute) of the target's maximum health as damage every turn for [#] turn(s).
                                localDesc += $"Deals an additional {tempComponent.maximumValue.ToString()}%" +
                                             $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                             $"of the target's maximum health as damage every turn for " +
                                             $"{tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                            }
                            else
                            {
                                // EASY READ: Deals an additional [# - #] (+##% Attribute) damage every turn for [#] turn(s).
                                localDesc += $"Deals an additional {tempComponent.minimumValue.ToString()} - {tempComponent.maximumValue.ToString()}" +
                                             $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                             $"damage every turn for {tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                            }
                        }
                        else if (tempComponent.StatusType == StatusType.Regenerate)
                        {
                            if (tempComponent.usePercentage)
                            {
                                // EASY READ: Heals for an additional [##%] (+##% Attribute) of the caster's maximum health every turn for [#] turn(s).
                                localDesc += $"Heals for an additional {tempComponent.maximumValue.ToString()}%" +
                                             $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                             $"of the caster's maximum health every turn for " +
                                             $"{tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                            }
                            else
                            {
                                // EASY READ: Heals for an additional [#] - [#] (+##% Attribute) health every turn for [#] turn(s).
                                localDesc += $"Heals for an additional {tempComponent.minimumValue.ToString()} - {tempComponent.maximumValue.ToString()}" +
                                             $"{(tempComponent.attributeType != AttributeType.None ? attributeText : string.Empty)} " +
                                             $"health every turn for {tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                            }
                        }
                        else if (tempComponent.StatusType == StatusType.Stun)
                        {
                            localDesc += $"Stuns the target for {tempComponent.duration.ToString()} {(tempComponent.duration > 1 ? "turns" : "turn")}. ";
                        }
                    }
                }
            }

            return localDesc;
        }

        private string DetermineAttributeTypeAndColor(AttributeType type, float percentage)
        {
            if (type == AttributeType.None || percentage <= 0) return string.Empty;
            var colorText = string.Empty;

            switch(type)
            {
                case AttributeType.Strength:
                    colorText = "#E17D00";
                    break;
                case AttributeType.Dexterity:
                    colorText = "#00C800";
                    break;
                case AttributeType.Constitution:
                    colorText = "#E10000";
                    break;
                case AttributeType.Intelligence:
                    colorText = "#00A0FF";
                    break;
                case AttributeType.Armor:
                    colorText = "#969696";
                    break;
                default:
                    return string.Empty;
            }

            return $" <color={colorText}>(+{Mathf.RoundToInt(percentage * 100f).ToString()}% {type.ToString()})</color>";
        }
    }
}