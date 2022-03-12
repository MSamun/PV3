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

using PV3.Characters.Common;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Skills;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.UI.Tooltip
{
    public static class TooltipDescriptionManager
    {
        private static AttributesObject _attributes;

        public static string GrabSpellDescription(SpellObject spell, AttributesObject attr)
        {
            if (!spell || !attr)
                return string.Empty;

            var localDesc = string.Empty;
            _attributes = attr;

            for (var i = 0; i < spell.components.Count; i++)
            {
                if (!spell.components[i]) continue;

                string attributeBonus = CalculateAttributeBonus
                (
                    spell.components[i].attributeType,
                    spell.components[i].attributePercentage,
                    spell.components[i].usePercentage
                );

                var minimumValue = spell.components[i].minimumValue.ToString();
                var maximumValue = spell.components[i].maximumValue.ToString();
                string valueDesc = spell.components[i].usePercentage ? $"{maximumValue}%" : (maximumValue == minimumValue ? $"[{maximumValue}]" : $"[{minimumValue} - {maximumValue}]");

                if (spell.components[i] is DamageComponent)
                {
                    var comp = spell.components[i] as DamageComponent;

                    // FORMAT: Deals [# - #](+AttributeType) damage. Attacks [#] times. Heal for [##%] of damage dealt.
                    localDesc += $"Deals {valueDesc}{attributeBonus} damage. " +
                                 $"{(comp.numberOfAttacks > 1 ? $"Attacks {comp.numberOfAttacks.ToString()} times. " : string.Empty)}" +
                                 $"{(comp.healPercentage > 0 ? $"Heal for {Mathf.RoundToInt(comp.healPercentage * 100f).ToString()}% of damage dealt. " : string.Empty)}";
                }
                else if (spell.components[i] is HealComponent)
                {
                    // FORMAT: Heals for ([# - #] OR [##%])(+AttributeType) [of maximum] health.
                    localDesc += $"Heals for {valueDesc}{attributeBonus} {(spell.components[i].usePercentage ? "of maximum " : string.Empty)}health. ";
                }
                else if (spell.components[i] is StatusComponent)
                {
                    var comp = spell.components[i] as StatusComponent;

                    var effectDesc = $"{(comp.isDebuff ? "Decreases" : "Increases")}";
                    var casterDesc = $"{(comp.applyOnCaster ? "caster's" : "target's")}";
                    var nonUniqueStatusEffectDesc = $"{effectDesc} the {casterDesc}";

                    // Linger: Deals an additional; Regenerate: Heals for an additional.
                    var uniqueStatusEffectDesc = $"{(comp.StatusType == StatusType.Linger ? "Deals" : "Heals for")} an additional";
                    var turnDesc = $"{comp.duration.ToString()} turn{(comp.duration > 1 ? "s" : string.Empty)}";

                    // Damage, Block, Dodge, DamageReduction, and Critical Status Types.
                    if (!comp.isUnique)
                    {
                        string typeDesc = comp.StatusType == StatusType.DamageReduction ? "Damage Reduction" : $"{comp.StatusType.ToString()}{(comp.StatusType != StatusType.Damage ? " Chance" : string.Empty)}";

                        // FORMAT: [Increases the caster's/Decreases the target's] [StatusType] by [##%](+AttributeType) for [#] turn[s].
                        localDesc += $"{nonUniqueStatusEffectDesc} {typeDesc} by {valueDesc}{attributeBonus} for {turnDesc}. ";
                    }
                    else
                    {
                        if (comp.StatusType == StatusType.Stun)
                        {
                            localDesc += $"Stuns the target for {turnDesc}. ";
                        }
                        else
                        {
                            string percentHealthDesc = comp.StatusType switch
                            {
                                StatusType.Linger => $"{(comp.usePercentage ? "of maximum health as " : string.Empty)}damage",
                                StatusType.Regenerate => $"{(comp.usePercentage ? "of maximum " : string.Empty)}health",
                                _ => string.Empty
                            };

                            // FORMAT - Linger: Deals ([# - #] OR [##%])(+AttributeType) [of maximum health as] damage every turn for [#] turn(s).
                            // FORMAT - Regenerate: Heals for  ([# - #] OR [##%])(+AttributeType) [of maximum] health every turn for [#] turn(s).
                            localDesc += $"{uniqueStatusEffectDesc} {valueDesc}{attributeBonus} {percentHealthDesc} every turn for {turnDesc}. ";
                        }
                    }
                }
            }

            return localDesc;
        }

        public static string GrabStatusEffectDescription(StatusEffect effect)
        {
            if (!effect.inUse) return string.Empty;

            var localDesc = string.Empty;

            if (!effect.isUnique)
            {
                var nonUniqueDesc = $"{(effect.isDebuff ? "Decreases" : "Increases")}";

                switch (effect.type)
                {
                    case StatusType.Damage:
                        localDesc = $"{nonUniqueDesc} the damage Spells deal by <color=#FFE1A5>{effect.bonusAmount.ToString()}%</color>.";
                        break;
                    case StatusType.DamageReduction:
                        localDesc = $"{(effect.isDebuff ? "Increases" : "Decreases")} the amount of damage taken from any source by <color=#FFE1A5>{effect.bonusAmount.ToString()}%</color>.";
                        break;
                    case StatusType.Lifesteal:
                        localDesc = $"{nonUniqueDesc} Lifesteal by <color=#FFE1A5>{effect.bonusAmount.ToString()}%</color>.";
                        break;
                    case StatusType.Block:
                    case StatusType.Dodge:
                    case StatusType.Critical:
                        localDesc = $"{nonUniqueDesc} {effect.type.ToString()} Chance by <color=#FFE1A5>{effect.bonusAmount.ToString()}%</color>.";
                        break;
                }
            }
            else
            {
                switch (effect.type)
                {
                    case StatusType.Stun:
                        localDesc = "Automatically skips your turn.";
                        break;
                    case StatusType.Linger:
                        localDesc = $"Deals <color=#E10000>{effect.bonusAmount.ToString()}{(effect.isPercentage ? "% of maximum Health</color> as" : "</color>")} damage at the end of your turn.";
                        break;
                    case StatusType.Regenerate:
                        localDesc = $"Heals for <color=#E10000>{effect.bonusAmount.ToString()}{(effect.isPercentage ? "% of maximum" : string.Empty)} Health</color> at the beginning of your turn.";
                        break;
                }
            }

            return localDesc;
        }

        public static string GrabSkillDescription(SkillsObject skill = null)
        {
            if (!skill) return string.Empty;

            var localDesc = string.Empty;

            for (var i = 0; i < skill.Components.Count; i++)
            {
                if (!skill.Components[i]) continue;

                var newEffect = new StatusEffect
                (
                    skill.Components[i].StatusType == StatusType.Random ? skill.Components[i].ChooseRandomStatusType() : skill.Components[i].StatusType,
                    skill.Components[i].GetRandomValueBetweenRange(),
                    skill.Components[i].duration,
                    skill.Components[i].isDebuff,
                    skill.Components[i].usePercentage,
                    skill.Components[i].isUnique,
                    true
                );

                // Skill Tooltips need to make it clear that any Skill that has a Linger Status Effect is applied on you, not the Enemy.
                if (skill.Components[i].StatusType == StatusType.Linger && skill.Components[i].applyOnCaster)
                    localDesc = $"{localDesc}Take <color=#E10000>{newEffect.bonusAmount.ToString()}{(newEffect.isPercentage ? "% of maximum Health</color> as" : "</color>")} damage at the end of your turn. ";
                else
                    localDesc = $"{localDesc}{GrabStatusEffectDescription(newEffect)} ";
            }

            return localDesc;
        }

        private static string CalculateAttributeBonus(AttributeType type, float percent, bool isPercentHealth = false)
        {
            if (type == AttributeType.None || percent <= 0) return string.Empty;

            var colorText = string.Empty;
            var baseAttributeValue = 0;

            if (type == AttributeType.Strength)
            {
                colorText = "#E17D00";
                baseAttributeValue = _attributes.Strength;
            }
            else if (type == AttributeType.Dexterity)
            {
                colorText = "#00C800";
                baseAttributeValue = _attributes.Dexterity;
            }
            else if (type == AttributeType.Constitution)
            {
                colorText = "#E10000";
                baseAttributeValue = _attributes.Constitution;
            }
            else if (type == AttributeType.Intelligence)
            {
                colorText = "#00A0FF";
                baseAttributeValue = _attributes.Intelligence;
            }
            else if (type == AttributeType.Armor)
            {
                colorText = "#969696";
                baseAttributeValue = _attributes.Armor;
            }

            int finalAttributeValue = Mathf.FloorToInt(baseAttributeValue * percent);
            return $"<color={colorText}>(+{finalAttributeValue + (isPercentHealth ? "%" : string.Empty)})</color>";
        }
    }
}