using PV3.Character;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.UI.SpellDescription
{
    public static class SpellDescriptionManager
    {
        private static AttributesObject Attributes;

        // When displaying Spell and Status Effect Tooltips, damage calculations are made and shown in the description.
        // We need a reference of the Character's Attribute information.
        // Originally, the Spell Tooltip would have showed this: [4 - 6](+50% Strength). Assuming that the Character's Strength is equal to 7, it now shows this:
        // [4 - 6](+3). It always rounds down.

        public static string SetDescription(SpellObject spell, AttributesObject attributes)
        {
            Attributes = attributes;
            var localDesc = string.Empty;

            for (var i = 0; i < spell.components.Count; i++)
            {
                if (!spell.components[i]) continue;

                var attributeBonusDesc = CalculateAndDisplayAttributeBonus(spell.components[i].attributeType, spell.components[i].attributePercentage,
                    spell.components[i].usePercentage);
                var minimumValueDesc = spell.components[i].minimumValue.ToString();
                var maximumValueDesc = spell.components[i].maximumValue.ToString();
                var valueDisplayModifierDesc = spell.components[i].usePercentage ? $"{maximumValueDesc}%" : $"[{minimumValueDesc} - {maximumValueDesc}]";

                if (spell.components[i] is DamageComponent)
                {
                    var comp = spell.components[i] as DamageComponent;

                    // FORMAT: Deals [# - #](+AttributeType) damage. Attacks [#] times.
                    localDesc += $"Deals {valueDisplayModifierDesc}{attributeBonusDesc} damage. " +
                                 $"{(comp.numberOfAttacks > 1 ? $"Attacks {comp.numberOfAttacks.ToString()} times. " : string.Empty)}";
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
                        var typeDesc = string.Empty;
                        if (comp.StatusType == StatusType.DamageReduction)

                        {
                            typeDesc = "Damage Reduction";
                        }
                        else
                        {
                            typeDesc = $"{comp.StatusType.ToString()}{(comp.StatusType != StatusType.Damage ? " Chance" : string.Empty)}";
                        }

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
                baseAttributeValue = Attributes.Strength;
            }
            else if (type == AttributeType.Dexterity)
            {
                colorText = "#00C800";
                baseAttributeValue = Attributes.Dexterity;
            }
            else if (type == AttributeType.Constitution)
            {
                colorText = "#E10000";
                baseAttributeValue = Attributes.Constitution;
            }
            else if (type == AttributeType.Intelligence)
            {
                colorText = "#00A0FF";
                baseAttributeValue = Attributes.Intelligence;
            }
            else if (type == AttributeType.Armor)
            {
                colorText = "#969696";
                baseAttributeValue = Attributes.Armor;
            }

            var finalAttributeValue = Mathf.FloorToInt(baseAttributeValue * percent);
            return $"<color={colorText}>(+{finalAttributeValue + (isPercentHealth ? "%" : string.Empty)})</color>";
        }
    }
}