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
using PV3.ScriptableObjects.Character;
using PV3.ScriptableObjects.Game;
using PV3.ScriptableObjects.Spells;
using UnityEngine;

namespace PV3.Character.Spells
{
    public class EnemyStanceLogic : MonobehaviourReference
    {
        [SerializeField] private IntValue SpellIndex;
        [SerializeField] private GameEventObject OnChosenSpellEvent;

        private EnemyObject _enemy;
        private const int NO_SPELL_AVAILABLE = -1;
        private int _localIndex;
        [SerializeField] private List<int> _availableSpells = new List<int>();

        public void SetCurrentEnemy()
        {
            _enemy = GameStateManager.CurrentEnemy;
        }

        public void DetermineSpellBasedOffStance()
        {
            if (GameStateManager.CurrentGameState != GameStateManager.GameState.EnemyTurn) return;

            _localIndex = GrabIndexOfSpellType(SpellType.Heal);
            _availableSpells.Clear();

            // Use a Healing Spell if Enemy has less than half health AND there is a Healing Spell available to use.
            if (EnemyHasLessThanHalfHealth() && _localIndex != NO_SPELL_AVAILABLE)
            {
                StartCoroutine(ExecuteMandatoryHealSpell());
                return;
            }

            StartCoroutine(ExecuteEnemySpellLogic());
            // switch (_enemy.Stance)
            // {
            //     case Stance.Aggressive:
            //         StartCoroutine(ExecuteAggressiveStanceLogic());
            //         break;
            //     case Stance.Defensive:
            //         StartCoroutine(ExecuteDefensiveStanceLogic());
            //         break;
            //     case Stance.Balanced:
            //         StartCoroutine(ExecuteBalancedStanceLogic());
            //         break;
            //     default:
            //         print("Default message in Enemy Stance!");
            //         return;
            // }
        }

        private IEnumerator ExecuteMandatoryHealSpell()
        {
            yield return new WaitForSeconds(1.75f);
            SetChosenSpell();
            yield return null;
        }

        private IEnumerator ExecuteEnemySpellLogic()
        {
            yield return new WaitForSeconds(1.75f);
            PopulateListOfAvailableSpells();

            if (_availableSpells.Count > 0)
            {
                _localIndex = _availableSpells[Random.Range(0, _availableSpells.Count)];
                SetChosenSpell();
            }

            yield return null;
        }

        // private IEnumerator ExecuteAggressiveStanceLogic()
        // {
        //     yield return new WaitForSeconds(1.75f);
        //     PopulateListOfAvailableSpells();
        //
        //     // Enemy will choose a random Spell in the List of available Spells.
        //     if (_availableSpells.Count > 0)
        //     {
        //         _localIndex = _availableSpells[Random.Range(0, _availableSpells.Count)];
        //         SetChosenSpell();
        //     }
        //
        //     yield return null;
        // }
        //
        // private IEnumerator ExecuteDefensiveStanceLogic()
        // {
        //     yield return new WaitForSeconds(1.75f);
        //     PopulateListOfAvailableSpells();
        //
        //     // Enemy will choose a random Spell in the List of available Spells.
        //     if (_availableSpells.Count > 0)
        //     {
        //         _localIndex = _availableSpells[Random.Range(0, _availableSpells.Count)];
        //         SetChosenSpell();
        //     }
        //     yield return null;
        // }
        //
        // private IEnumerator ExecuteBalancedStanceLogic()
        // {
        //     yield return new WaitForSeconds(1.75f);
        //     PopulateListOfAvailableSpells();
        //
        //     // Enemy will choose a random Spell in the List of available Spells.
        //     if (_availableSpells.Count > 0)
        //     {
        //         _localIndex = _availableSpells[Random.Range(0, _availableSpells.Count)];
        //         SetChosenSpell();
        //     }
        //     yield return null;
        // }

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
                if (_enemy.SpellsListObject.SpellsList[i].spell.spellType == SpellType.Heal && !EnemyHasLessThanHalfHealth()) continue;
                if (_enemy.SpellsListObject.IsSpellOnCooldown(i)) continue;
                if (!EnemyHasEnoughStaminaForSpell(i)) continue;
                if (_enemy.SpellsListObject.SpellsList[i].spell.spellType == SpellType.Status && !EnemyCanUseAStatusEffectSpell(i)) continue;

                _availableSpells.Add(i);
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
            // Grabs the first Spell that meets these requirements.
            for (var i = 0; i < _enemy.SpellsListObject.SpellsList.Count; i++)
            {
                if (_enemy.SpellsListObject.SpellsList[i].spell.spellType != type || _enemy.SpellsListObject.IsSpellOnCooldown(i)) continue;
                return i;
            }

            return NO_SPELL_AVAILABLE;
        }

        private bool EnemyHasLessThanHalfHealth()
        {
            return _enemy.CurrentHealth.Value <= _enemy.MaxHealth.Value / 2;
        }

        private bool EnemyHasEnoughStaminaForSpell(int index)
        {
            return _enemy.CurrentStamina.Value >= _enemy.SpellsListObject.SpellsList[index].spell.staminaCost;
        }

        private bool EnemyCanUseAStatusEffectSpell(int index)
        {
            // Enemy can only use a Status Effect Spell if:
            //      - It applies a debuff to the Player.
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
            for (var i = 0; i < _enemy.SpellsListObject.SpellsList[index].spell.components.Count; i++)
            {
                if (!_enemy.SpellsListObject.SpellsList[index].spell.components[i]) continue;

                var comp = _enemy.SpellsListObject.SpellsList[index].spell.components[i] as StatusComponent;
                if (!comp.isDebuff && comp.applyOnCaster) doesSpellApplyBuff = true;
            }

            // IF ENEMY HAS A BUFF AND TRIES TO USE BUFF                -> FALSE
            // IF ENEMY HAS A BUFF AND DOES NOT TRY TO USE BUFF         -> TRUE
            // IF ENEMY DOES NOT HAVE BUFF AND TRIES TO USE BUFF        -> TRUE
            // IF ENEMY DOES NOT HAVE BUFF AND DOES NOT TRY TO USE BUFF -> TRUE
            print((!hasBuffsActive || !doesSpellApplyBuff).ToString());
            return !hasBuffsActive || !doesSpellApplyBuff;
        }
    }
}