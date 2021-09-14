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
using PV3.ScriptableObjects.Spells;
using PV3.ScriptableObjects.UI;
using UnityEngine;

namespace PV3.Characters.Common
{
    public abstract class ApplyCharacterUniqueEffects : MonobehaviourReference
    {
        [SerializeField] protected CharacterObject Character;
        [SerializeField] private FloatingTextObject FloatingTextObject;
        [SerializeField] private TurnNotificationObject TurnNotifObject;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnCharacterEndTurnEvent;

        [SerializeField] private GameEventObject OnCharacterNotStunnedEvent;
        [SerializeField] private GameEventObject OnCharacterDisplayFloatingTextEvent;

        private int FindStatusEffectOfType(StatusType type)
        {
            for (var i = 0; i < Character.StatusEffectObject.CurrentStatusEffects.Count; i++)
            {
                if (!Character.StatusEffectObject.CurrentStatusEffects[i].inUse) continue;
                if (Character.StatusEffectObject.CurrentStatusEffects[i].type == type) return i;
            }

            return -1;
        }

        // The Regenerate Effect gets applied at the beginning of a Character's Turn.
        public void ApplyRegenerateEffect()
        {
            if (Character.CurrentHealth.Value <= 0) return;

            var index = FindStatusEffectOfType(StatusType.Regenerate);
            if (index == -1) return;

            CalculateUniqueStatusEffectValue(index, true);
        }

        // The Linger Effect gets applied at the end of a Character's Turn.
        public void ApplyLingerEffect()
        {
            var index = FindStatusEffectOfType(StatusType.Linger);
            if (index == -1) return;

            CalculateUniqueStatusEffectValue(index, false);
        }

        private void CalculateUniqueStatusEffectValue(int index, bool isRegen)
        {
            // Either deal [# - #] damage or deal damage equal to [# - #]% of Maximum Health.
            var value = Character.StatusEffectObject.CurrentStatusEffects[index].isPercentage ? Mathf.RoundToInt(Character.MaxHealth.Value * (Character.StatusEffectObject.CurrentStatusEffects[index].bonusAmount / 100f)) : Character.StatusEffectObject.CurrentStatusEffects[index].bonusAmount;

            FloatingTextObject.SetFloatingTextColorAndValue(isRegen ? FloatingTextObject.HealColor : FloatingTextObject.DamageColor, value.ToString());
            OnCharacterDisplayFloatingTextEvent.Raise();

            if (isRegen)
                Character.AddHealth(value);
            else
                Character.DeductHealth(value);

            Character.StatusEffectObject.DecrementStatusEffectDuration(index);
        }

        public void CheckIfStunned()
        {
            var index = FindStatusEffectOfType(StatusType.Stun);
            if (index == -1)
                OnCharacterNotStunnedEvent.Raise();
            else
                StartCoroutine(ApplyStunEffect(index));
        }

        private IEnumerator ApplyStunEffect(int index)
        {
            TurnNotifObject.UpdateDescriptionToStunned(Character);
            yield return new WaitForSeconds(1.5f);

            OnCharacterEndTurnEvent.Raise();
            Character.StatusEffectObject.DecrementStatusEffectDuration(index);
        }
    }
}