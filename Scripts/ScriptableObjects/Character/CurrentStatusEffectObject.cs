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

using System.Collections.Generic;
using PV3.Character.StatusEffects;
using PV3.ScriptableObjects.Game;
using UnityEngine;

namespace PV3.ScriptableObjects.Character
{
    // The asterisk (*) implies that only one copy of this object is needed. A singleton might be good in this situation, but since I'm the only one working on this game
    // it's more of a visual reminder for me.
    [CreateAssetMenu(fileName = "New Status Effect Object", menuName = "Character/Status Effects List*")]
    public class CurrentStatusEffectObject : ScriptableObject
    {
        public CharacterGameBonuses BonusObject;

        [Header("Game Events")]
        [SerializeField] private GameEventObject OnStatusEffectAppliedEvent;

        [SerializeField] private GameEventObject OnStatusEffectDeductedEvent;

        [Header("")]
        public List<StatusEffect> CurrentStatusEffects = new List<StatusEffect>();

        public void AddStatusEffect(StatusEffect newEffect)
        {
            var index = CheckIfThereIsEmptySlotInList();

            if (index == -1)
                CurrentStatusEffects.Add(newEffect);
            else
                CurrentStatusEffects[index] = newEffect;

            ApplyStatusEffectBonus(newEffect);
        }

        public void ResetStatusEffectList()
        {
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
                if (i > 2)
                    CurrentStatusEffects.RemoveAt(i);
                else
                    RemoveStatusEffect(i);

            OnStatusEffectDeductedEvent.Raise();
        }

        // Player Buffs and Enemy Debuffs durations get decremented at the beginning of the Player's Turn.
        // Enemy Buffs and Player Debuffs durations get decremented at the beginning of the Enemy's Turn.
        // Hence, the two separate methods.
        public void DeductAllBuffStatusEffects()
        {
            // Unique Status Effects (Regenerate, Linger, Stun) durations get decremented immediately after being applied.
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (!CurrentStatusEffects[i].inUse || CurrentStatusEffects[i].isUnique || CurrentStatusEffects[i].isDebuff) continue;
                DecrementStatusEffectDuration(i);
            }

            OnStatusEffectDeductedEvent.Raise();
        }

        public void DeductAllDebuffStatusEffects()
        {
            // Unique Status Effects (Regenerate, Linger, Stun) durations get decremented immediately after being applied.
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (!CurrentStatusEffects[i].inUse || CurrentStatusEffects[i].isUnique || !CurrentStatusEffects[i].isDebuff) continue;
                DecrementStatusEffectDuration(i);
            }

            OnStatusEffectDeductedEvent.Raise();
        }

        public void DecrementStatusEffectDuration(int index)
        {
            CurrentStatusEffects[index].duration--;
            if (CurrentStatusEffects[index].duration != 0) return;

            BonusObject.ResetBonusToZero(CurrentStatusEffects[index].type);
            RemoveStatusEffect(index);
            OnStatusEffectDeductedEvent.Raise();
        }

        private void RemoveStatusEffect(int index = -1)
        {
            if (index == -1) return;
            CurrentStatusEffects[index] = new StatusEffect();
        }

        private int CheckIfThereIsEmptySlotInList()
        {
            for (var i = 0; i < CurrentStatusEffects.Count; i++)
            {
                if (CurrentStatusEffects[i].inUse) continue;
                return i;
            }

            return -1;
        }

        private void ApplyStatusEffectBonus(StatusEffect effect)
        {
            BonusObject.InfluenceBonus(effect.isDebuff, effect.type, effect.bonusAmount);
            OnStatusEffectAppliedEvent.Raise();
        }
    }
}