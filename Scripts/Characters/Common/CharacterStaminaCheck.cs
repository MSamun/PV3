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
using PV3.Miscellaneous;
using PV3.ScriptableObjects.Characters;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.Characters.Common
{
    public abstract class CharacterStaminaCheck : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;

        [Header("Game Event Objects")]
        [SerializeField] private GameEventObject OnCharacterHasEnoughStaminaEvent;

        [SerializeField] private GameEventObject OnCharacterEndTurnEvent;

        public void CheckCurrentStamina()
        {
            StartCoroutine(StaminaCoroutine());
        }

        private IEnumerator StaminaCoroutine()
        {
            yield return new WaitForSeconds(0.75f);

            var hasEnoughStaminaForSpell = false;

            for (var i = 0; i < Character.SpellsListObject.SpellsList.Count; i++)
            {
                if (Character.SpellsListObject.SpellsList[i].Spell.staminaCost > Character.CurrentStamina.Value || Character.SpellsListObject.SpellsList[i].IsOnCooldown) continue;

                hasEnoughStaminaForSpell = true;
                break;
            }

            if (hasEnoughStaminaForSpell)
                OnCharacterHasEnoughStaminaEvent.Raise();
            else
                OnCharacterEndTurnEvent.Raise();

            StopAllCoroutines();
            yield return null;
        }
    }
}