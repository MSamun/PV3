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

using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.UI.SpellDescription
{
    public static class SpellDescriptionManager
    {
        private static AttributesObject attributes;

        // When displaying Spell and Status Effect Tooltips, damage calculations are made and shown in the description.
        // We need a reference of the Character's Attribute information.
        // Originally, the Spell Tooltip would have showed this: [4 - 6](+50% Strength). Assuming that the Character's Strength is equal to 7, it now shows this:
        // [4 - 6](+3). It always rounds down.

        public static string SetDescription(SpellObject spell, AttributesObject attribute)
        {
            if (!spell || !attribute)
            {
                Debug.LogError($"Error! SpellObject or AttributesObject is NULL. SPELL: {spell}, ATTRIBUTES: {attribute}");
                return string.Empty;
            }

            SpellDescriptionManager.attributes = attribute;
            var localDesc = string.Empty;

            for (var i = 0; i < spell.components.Count; i++)
            {
                if (!spell.components[i]) continue;

                var attributeBonusDesc = CalculateAndDisplayAttributeBonus
                (
                    spell.components[i].attributeType,
                    spell.components[i].attributePercentage,
                    spell.components[i].usePercentage
                );
                var minimumValueDesc = spell.components[i].minimumValue.ToString();
                var maximumValueDesc = spell.components[i].maximumValue.ToString();
                var valueDisplayModifierDesc = spell.components[i].usePercentage ? $"{maximumValueDesc}%" : $"[{minimumValueDesc} - {maximumValueDesc}]";

                if (spell.components[i] is DamageComponent)
                {
                    var comp = spell.components[i] as DamageComponent;

                    // FORMAT: Deals [# - #](+AttributeType) damage. Attacks [#] times. Heal for [##%] of damage dealt.
                    localDesc += $"Deals {valueDisplayModifierDesc}{attributeBonusDesc} damage. " +
                                 $"{(comp.numberOfAttacks > 1 ? $"Attacks {comp.numberOfAttacks.ToString()} times. " : string.Empty)}" +
                                 $"{(comp.healPercentage > 0 ? $"Heal for {Mathf.RoundToInt(comp.healPercentage * 100f).ToString()}% of damage dealt. " : string.Empty)}";
                }
                else if (spell.components[i] is HealComponent)
                {
                    // FORMAT: Heals for ([# - #] OR [##%])(+AttributeType) [of your maximum] health.
                    localDesc += $"Heals for {valueDisplayModifierDesc}{attributeBonusDesc} {(spell.components[i].usePercentage ? "of maximum " : string.Empty)}health. ";
                }
                else if (spell.components[i] is StatusComponent)
                {
                    var comp = spell.components[i] as StatusComponent;
                    var nonUniqueStatusEffectDesc = $"{(comp.isDebuff ? "Decreases the target's" : "Increases the caster's")}";

                    // Linger: Deals an additional; Regenerate: Heals for an additional.
                    var uniqueStatusEffectDesc = $"{(comp.StatusType == StatusType.Linger ? "Deals" : "Heals for")} an additional";
                    var turnDesc = $"{comp.duration.ToString()} turn{(comp.duration > 1 ? "s" : string.Empty)}";

                    // Damage, Block, Dodge, DamageReduction, and Critical StatusTypes.
                    if (!comp.isUnique)
                    {
                        var typeDesc = comp.StatusType == StatusType.DamageReduction ? "Damage Reduction" : $"{comp.StatusType.ToString()}{(comp.StatusType != StatusType.Damage ? " Chance" : string.Empty)}";

                        // FORMAT: [Increases your/Decreases the target's] [StatusType] by [##%](+AttributeType) for [#] turn[s].
                        localDesc += $"{nonUniqueStatusEffectDesc} {typeDesc} by {valueDisplayModifierDesc}{attributeBonusDesc} for {turnDesc}. ";
                    }
                    else
                    {
                        if (comp.StatusType == StatusType.Stun)
                        {
                            localDesc += $"Stuns the target for {turnDesc}. ";
                        }
                        else
                        {
                            var percentHealthDesc = comp.StatusType switch
                            {
                                StatusType.Linger => $"{(comp.usePercentage ? "of maximum health as " : string.Empty)}damage",
                                StatusType.Regenerate => $"{(comp.usePercentage ? "of maximum " : string.Empty)}health",
                                _ => string.Empty
                            };

                            // FORMAT - Linger: Deals ([# - #] OR [##%])(+AttributeType) [of maximum health as] damage every turn for [#] turn(s).
                            // FORMAT - Regenerate: Heals for  ([# - #] OR [##%])(+AttributeType) [of maximum] health every turn for [#] turn(s).
                            localDesc += $"{uniqueStatusEffectDesc} {valueDisplayModifierDesc}{attributeBonusDesc} {percentHealthDesc} every turn for {turnDesc}. ";
                        }
                    }
                }
            }

            return localDesc;
        }

        private static string CalculateAndDisplayAttributeBonus(AttributeType type, float percent, bool isPercentHealth = false)
        {
            if (type == AttributeType.None || percent <= 0) return string.Empty;

            var colorText = string.Empty;
            var baseAttributeValue = 0;

            if (type == AttributeType.Strength)
            {
                colorText = "#E17D00";
                baseAttributeValue = attributes.Strength;
            }
            else if (type == AttributeType.Dexterity)
            {
                colorText = "#00C800";
                baseAttributeValue = attributes.Dexterity;
            }
            else if (type == AttributeType.Constitution)
            {
                colorText = "#E10000";
                baseAttributeValue = attributes.Constitution;
            }
            else if (type == AttributeType.Intelligence)
            {
                colorText = "#00A0FF";
                baseAttributeValue = attributes.Intelligence;
            }
            else if (type == AttributeType.Armor)
            {
                colorText = "#969696";
                baseAttributeValue = attributes.Armor;
            }

            var finalAttributeValue = Mathf.FloorToInt(baseAttributeValue * percent);
            return $"<color={colorText}>(+{finalAttributeValue + (isPercentHealth ? "%" : string.Empty)})</color>";
        }
    }
}