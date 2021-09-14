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

using System.Collections;
using System.Collections.Generic;
using PV3.Game;
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.Characters.Enemy
{
    public class EnemyLogic : MonobehaviourReference
    {
        [SerializeField] private IntValue SpellIndex;
        [SerializeField] private GameEventObject OnChosenSpellEvent;
        [SerializeField] private GameEventObject OnEnemyEndTurnEvent;

        private EnemyObject _enemy;
        private const int NO_SPELL_AVAILABLE = -1;
        private int _localIndex;

        [Header("")]
        [SerializeField] private List<int> AvailableSpells = new List<int>();

        public void SetCurrentEnemy()
        {
            _enemy = GameStateManager.CurrentEnemy;
        }

        public void DetermineSpell()
        {
            if (GameStateManager.CurrentGameState != GameState.EnemyTurn) return;

            _localIndex = GrabIndexOfSpellType(SpellType.Heal);
            AvailableSpells.Clear();

            // Use a Healing Spell if Enemy has less than half health AND there is a Healing Spell available to use.
            if (EnemyHasLessThanHalfHealth() && _localIndex != NO_SPELL_AVAILABLE)
            {
                StartCoroutine(ExecuteMandatoryHealSpell());
                return;
            }

            StartCoroutine(ExecuteEnemySpellLogic());
        }

        private IEnumerator ExecuteMandatoryHealSpell()
        {
            // The chosen Spell is set to a Healing-type Spell by default.
            yield return new WaitForSeconds(1.75f);
            SetChosenSpell();
            yield return null;
        }

        private IEnumerator ExecuteEnemySpellLogic()
        {
            yield return new WaitForSeconds(1.75f);
            PopulateListOfAvailableSpells();

            if (AvailableSpells.Count > 0)
            {
                _localIndex = AvailableSpells[Random.Range(0, AvailableSpells.Count)];
                SetChosenSpell();
            }
            else
            {
                OnEnemyEndTurnEvent.Raise();
            }

            yield return null;
        }

        private void PopulateListOfAvailableSpells()
        {
            for (var i = 0; i < _enemy.SpellsListObject.SpellsList.Count; i++)
            {
                // Splitting this up into multiple if statements strictly for readability.
                // The conditions are rather self-explanatory, but I am going to explain what checks are being made just for the sake of transparency:
                //      - Enemy cannot use a Healing Spell when it has 50% or more Health.
                //      - Enemy cannot use a Spell that is on cooldown.
                //      - Enemy cannot use a Spell that it does not have enough Stamina for.
                //      - Enemy cannot use a Spell that grants itself a Buff when the Enemy already has a Buff.
                if (_enemy.SpellsListObject.SpellsList[i].Spell.spellType == SpellType.Heal && !EnemyHasLessThanHalfHealth()) continue;
                if (_enemy.SpellsListObject.IsSpellOnCooldown(i)) continue;
                if (!EnemyHasEnoughStaminaForSpell(i)) continue;
                if (_enemy.SpellsListObject.SpellsList[i].Spell.spellType == SpellType.Status && !EnemyCanUseAStatusEffectSpell(i)) continue;

                AvailableSpells.Add(i);
            }
        }

        private void SetChosenSpell()
        {
            SpellIndex.Value = _localIndex;
            OnChosenSpellEvent.Raise();
            StopAllCoroutines();
        }

        private int GrabIndexOfSpellType(SpellType type)
        {
            if (_enemy.SpellsListObject.SpellsList.Count > 0)
            {
                // Grabs the first Spell that meets these requirements.
                for (var i = 0; i < _enemy.SpellsListObject.SpellsList.Count; i++)
                {
                    if (_enemy.SpellsListObject.SpellsList[i].Spell.spellType != type || _enemy.SpellsListObject.IsSpellOnCooldown(i)) continue;
                    return i;
                }
            }

            return NO_SPELL_AVAILABLE;
        }

        private bool EnemyHasLessThanHalfHealth()
        {
            return _enemy.CurrentHealth.Value <= _enemy.MaxHealth.Value / 2;
        }

        private bool EnemyHasEnoughStaminaForSpell(int index)
        {
            return _enemy.CurrentStamina.Value >= _enemy.SpellsListObject.SpellsList[index].Spell.staminaCost;
        }

        private bool EnemyCanUseAStatusEffectSpell(int index)
        {
            // Enemy can only use a Status Effect Spell if:
            //      - It applies a Debuff to the Player.
            //      - It applies a buff to itself while it currently does not have any buffs active.
            var hasBuffsActive = false;
            var doesSpellApplyBuff = false;

            // First, we need to check if there are any buffs applied to the Enemy.
            for (var i = 0; i < _enemy.StatusEffectObject.CurrentStatusEffects.Count; i++)
            {
                if (!_enemy.StatusEffectObject.CurrentStatusEffects[i].inUse || _enemy.StatusEffectObject.CurrentStatusEffects[i].isDebuff) continue;
                hasBuffsActive = true;
            }

            // Then, we check to see if the Spell applies a buff.
            for (var i = 0; i < _enemy.SpellsListObject.SpellsList[index].Spell.components.Count; i++)
            {
                if (!_enemy.SpellsListObject.SpellsList[index].Spell.components[i]) continue;

                var comp = _enemy.SpellsListObject.SpellsList[index].Spell.components[i] as StatusComponent;

                if (comp)
                {
                    if (!comp.isDebuff && comp.applyOnCaster)
                        doesSpellApplyBuff = true;
                }
            }

            // IF ENEMY HAS A BUFF AND TRIES TO USE BUFF                -> FALSE
            // IF ENEMY HAS A BUFF AND DOES NOT TRY TO USE BUFF         -> TRUE
            // IF ENEMY DOES NOT HAVE BUFF AND TRIES TO USE BUFF        -> TRUE
            // IF ENEMY DOES NOT HAVE BUFF AND DOES NOT TRY TO USE BUFF -> TRUE
            return !hasBuffsActive || !doesSpellApplyBuff;
        }
    }
}