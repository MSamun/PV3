// PV3 is a menu-based RPG game.
// This file is part of the PV3 distribution (https://github.com/MSamun/PV3)
// Copyright (C) 2021-2022 Matthew Samun.
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

using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using PV3.ScriptableObjects.UI;
using UnityEngine;

namespace PV3.Characters.Common
{
    public abstract class CharacterSpellBehaviour : MonobehaviourReference
    {
        [Header("Character Objects")]
        [SerializeField] protected CharacterObject Caster;
        [SerializeField] protected CharacterObject Target;
        [SerializeField] protected IntValue CasterSpellIndex;

        [Header("Floating Text Objects")]
        [SerializeField] private FloatingTextObject FloatTextObject;
        [SerializeField] private TurnNotificationObject TurnNotifObject;
        [SerializeField] private GameEventObject OnCasterDisplayFloatingTextEvent;
        [SerializeField] private GameEventObject OnTargetDisplayFloatingTextEvent;

        [Header("Spell VFX Objects")]
        [SerializeField] private SpellVFXMaterialsObject SpellVfxObject;
        [SerializeField] private GameEventObject OnCasterPlaySpellVfxEvent;
        [SerializeField] private GameEventObject OnTargetPlaySpellVfxEvent;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnCasterSpellDoneEvent;
        [SerializeField] private GameEventObject OnPlaySpellSfxEvent;

        private bool _canStatusEffectBeenApplied;
        private bool _isHealSpell;
        private int _totalSpellValue;
        private int _totalNumberOfBlocks;
        private int _totalNumberOfDodges;
        private int _totalNumberOfAttacks;
        public void CheckIfSpellComponentIsNullBeforeExecution()
        {
            _canStatusEffectBeenApplied = true;
            _isHealSpell = false;
            _totalSpellValue = 0;
            _totalNumberOfBlocks = 0;
            _totalNumberOfDodges = 0;
            _totalNumberOfAttacks = 1;

            SpellVfxObject.SetParticleEffect(Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.VfxMaterial);
            TurnNotifObject.ResetDescription();

            for (var i = 0; i < Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.components.Count; i++)
            {
                if (!Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.components[i]) continue;
                DetermineSpellComponent(Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.components[i]);
            }

            UpdateTurnNotificationDescription();

            //AudioManager.SpellSfx = Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].spell.SfxClip;
            OnPlaySpellSfxEvent.Raise();

            Caster.DeductStamina(Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.staminaCost);
            OnCasterSpellDoneEvent.Raise();
        }

        private void DetermineSpellComponent(SpellComponentObject component)
        {
            if (component is DamageComponent damageComponent)
                ExecuteDamageComponent(damageComponent);

            if (component is HealComponent healComponent)
                ExecuteHealComponent(healComponent);

            if (component is StatusComponent statusComponent)
                ExecuteStatusComponent(statusComponent);
        }

        private void ExecuteDamageComponent(DamageComponent component)
        {
            // Some Spells attack multiple times, so we need to only apply the Debuff Status Effect once.
            _canStatusEffectBeenApplied = false;

            // The Turn Notification Text needs to know whether an attack has been dodged or blocked.
            // This would be an easy fix if all the Spells only dealt damage once, but there are some Multi-Attack Spells.
            // This makes it so when a Multi-Attack Spell is being used, the Turn Notification Text will only say Blocked if all attacks are blocked.
            _totalNumberOfAttacks = component.numberOfAttacks;

            for (var i = 0; i < _totalNumberOfAttacks; i++)
            {
                int damage = CalculateSpellDamage(component);

                if (Target.HasBlockedAttack())
                {
                    FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.BlockedColor, "Blocked");
                    OnTargetDisplayFloatingTextEvent.Raise();
                    HealTargetFromBlockedAttack(damage);
                    _totalNumberOfBlocks++;
                }
                else if (Target.HasDodgedAttack())
                {
                    FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.DodgedColor, "Dodged");
                    OnTargetDisplayFloatingTextEvent.Raise();
                    DamageCasterFromDodgedAttack(damage);
                    _totalNumberOfDodges++;
                }
                else
                {
                    if (Caster.HasLandedCriticalStrike())
                    {
                        damage *= 2;
                        FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.DodgedColor, "CRITICAL!");
                        OnTargetDisplayFloatingTextEvent.Raise();
                    }

                    _canStatusEffectBeenApplied = true;
                    _totalSpellValue += damage;

                    Target.DeductHealth(damage);
                    FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.DamageColor, damage.ToString());
                    OnTargetDisplayFloatingTextEvent.Raise();
                    OnTargetPlaySpellVfxEvent.Raise();

                    if (component.healPercentage > 0) ApplyLifesteal(component.healPercentage, damage);
                }

                print($"Damage dealt: {damage.ToString()}");
            }
        }

        private int CalculateSpellDamage(SpellComponentObject component)
        {
            // Spell Components have a minimum and maximum value (# - #). This checks to see whether to see them as regular values or as percentages.
            // (i.e. either deal 5 - 7 damage or deal 5% - 7% of maximum Health damage.)
            int baseDamage = component.usePercentage ? component.GetPercentageOfValue(Target.MaxHealth.Value) : component.GetRandomValueBetweenRange();
            int attributeBonus = Mathf.FloorToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            int damageBonus = Mathf.FloorToInt((baseDamage + attributeBonus) * (Caster.GetDamageBonus() / 100f));

            int subtotalDamage = baseDamage + attributeBonus + damageBonus;
            int damageReduction = Mathf.FloorToInt(subtotalDamage * (Target.GetDamageReduction() / 100f));

            int totalDamage = Mathf.Clamp(subtotalDamage - damageReduction, 0, subtotalDamage - damageReduction + 1);
            return totalDamage;
        }

        private void HealTargetFromBlockedAttack(int damage = 0)
        {
            // Spells dealing a low amount of damage leads to having the Target heal for zero health; I rather have the Target heal for a minimum of 1 health.
            // Although, the Target should only heal for 1 health from a blocked attack when the damage dealt by the Spell does not equal to zero.
            int value = Mathf.FloorToInt(damage * 0.25f);
            if (value <= 0 && damage != 0) value = 1;

            Target.AddHealth(value);
            FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.HealColor, value.ToString());
            OnTargetDisplayFloatingTextEvent.Raise();
            OnTargetPlaySpellVfxEvent.Raise();
        }

        private void DamageCasterFromDodgedAttack(int damage = 0)
        {
            // Spell dealing a low amount of damage leads to having the Caster take zero damage; I rather have the Caster take a minimum of 1 damage.
            // Although, the Caster should only take 1 damage from a dodged attack when the damage dealt by the Spell does not equal to zero.
            int value = Mathf.FloorToInt(damage * 0.25f);
            if (value <= 0 && damage != 0) value = 1;

            Caster.DeductHealth(value);
            FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.DamageColor, value.ToString());
            OnCasterDisplayFloatingTextEvent.Raise();
            OnCasterPlaySpellVfxEvent.Raise();
        }

        private void ApplyLifesteal(float healPercentage, int damage = 0)
        {
            // Spell dealing a low amount of damage led to having the Caster heal zero health; I rather have the Caster heal for a minimum of 1 health.
            // Although, the Caster should only heal for 1 health from lifesteal when the damage dealt by the Spell does not equal to zero.
            float totalPercent = healPercentage + Caster.GetLifestealBonus() / 100f;
            int value = Mathf.FloorToInt(damage * totalPercent);
            if (value <= 0 && damage != 0) value = 1;

            Caster.AddHealth(value);
            FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.HealColor, value.ToString());
            OnCasterDisplayFloatingTextEvent.Raise();
        }

        private void ExecuteHealComponent(HealComponent component)
        {
            // Spell Components have a minimum and maximum value (# - #). This check is for whether to see them as regular values or as percentages.
            // (e.g. either heal for 5 - 7 Health or heal for 5% - 7% of Maximum Health.)
            int baseHeal = component.usePercentage ? component.GetPercentageOfValue(Caster.MaxHealth.Value) : component.GetRandomValueBetweenRange();
            int attributeBonus = Mathf.FloorToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            int healBonus = Mathf.FloorToInt((baseHeal + attributeBonus) * (Caster.GetDamageBonus() / 100f));

            int totalHeal = Mathf.Clamp(baseHeal + attributeBonus + healBonus, 0, baseHeal + attributeBonus + healBonus + 1);
            _totalSpellValue = totalHeal;
            _isHealSpell = true;

            Caster.AddHealth(totalHeal);
            FloatTextObject.SetFloatingTextColorAndValue(FloatTextObject.HealColor, totalHeal.ToString());
            OnCasterDisplayFloatingTextEvent.Raise();
            OnCasterPlaySpellVfxEvent.Raise();
        }

        private void ExecuteStatusComponent(StatusComponent component)
        {
            if (!_canStatusEffectBeenApplied) return;

            int attributeBonus = Mathf.FloorToInt(Caster.GetAttribute(component.attributeType) * component.attributePercentage);
            int totalBonus = component.GetRandomValueBetweenRange() + attributeBonus;

            CurrentStatusEffectObject statusEffectObject = component.applyOnCaster ? Caster.StatusEffectObject : Target.StatusEffectObject;

            var newEffect = new StatusEffect
            (
                component.StatusType == StatusType.Random ? component.ChooseRandomStatusType() : component.StatusType,
                totalBonus,
                component.duration,
                component.isDebuff,
                component.usePercentage,
                component.isUnique,
                true
            );

            statusEffectObject.AddStatusEffect(newEffect);
            FloatTextObject.SetFloatingTextColorAndValue(newEffect);

            if (component.applyOnCaster)
                OnCasterDisplayFloatingTextEvent.Raise();
            else
                OnTargetDisplayFloatingTextEvent.Raise();
        }

        private void UpdateTurnNotificationDescription()
        {
            if (_totalNumberOfBlocks == _totalNumberOfAttacks)
            {
                TurnNotifObject.UpdateDescriptionToBlocked(Caster is PlayerObject, Target.Name, Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.name);
            }
            else if (_totalNumberOfDodges == _totalNumberOfAttacks)
            {
                TurnNotifObject.UpdateDescriptionToDodged(Caster is PlayerObject, Target.Name, Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.name);
            }
            else
            {
                TurnNotifObject.UpdateDescription
                (
                    Caster.Name,
                    Target.Name,
                    Caster.SpellsListObject.SpellsList[CasterSpellIndex.Value].Spell.name,
                    _totalSpellValue,
                    Caster is PlayerObject,
                    _isHealSpell
                );
            }

        }
    }
}